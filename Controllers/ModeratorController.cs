using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.Infrastructure.Extensions;
using VideoGameLibrary.Web.ViewModels.Moderator;


namespace VideoGameLibrary.Controllers
{
	using static VideoGameLibrary.Common.NotificationMessagesConstants;


	[Authorize]
	public class ModeratorController : Controller
	{
		private readonly IModeratorService moderatorService;
		private readonly IUserService userService;

		public ModeratorController(IModeratorService moderatorService, IUserService userService)
		{
			this.moderatorService = moderatorService;
			this.userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> Become()
		{
			string? userId = User.GetId();
			bool isModerator = await moderatorService.ModeratorExistsByUserIdAsync(userId!);
			if (isModerator)
			{
				TempData[ErrorMessage] = "You're a moderator already!";

				return RedirectToAction("All", "Game");
			}

			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Become(ModeratorApplicationViewModel model)
		{
			string? userId = User.GetId();

			bool isModerator = await moderatorService.ModeratorExistsByUserIdAsync(userId!);
			if (isModerator)
			{
				TempData[ErrorMessage] = "You're a moderator already!";

				return RedirectToAction("All", "Game");
			}

			bool isPhoneNumberTaken =
				await moderatorService.ModeratorExistsByPhoneNumberAsync(model.PhoneNumber);
			if (isPhoneNumberTaken)
			{
				ModelState.AddModelError(nameof(model.PhoneNumber), "Moderator with the provided phone number already exists!");
			}

			if (!ModelState.IsValid)
			{
				var errors = ModelState
					.Where(x => x.Value.Errors.Count > 0)
					.Select(x => new { x.Key, x.Value.Errors })
				.ToArray();

				return View(model);
			}

			try
			{
				await moderatorService.Create(userId!, model);
				TempData[SuccessMessage] = "Applied successfully!";
			}
			catch (Exception)
			{
				TempData[ErrorMessage] =
					"Unexpected error occurred while trying to submit your form to become a moderator. Please try again later!";

				return View(model);
			}

			return RedirectToAction("All", "Game");
		}

		[HttpGet]
		public async Task<IActionResult> OptOut()
		{
			string userId = User.GetId()!;
			bool isModerator = await moderatorService.ModeratorExistsByUserIdAsync(userId!);

			if (!isModerator)
			{
				TempData[ErrorMessage] = "You need to be a moderator!";

				return RedirectToAction("All", "Game");
			}

			string moderatorId = await moderatorService.GetModeratorIdByUserIdAsync(userId);

			if (string.IsNullOrEmpty(moderatorId))
			{
				TempData[ErrorMessage] = "Moderator does not exist!";

				return RedirectToAction("All", "Game");
			}

			try
			{
				ModeratorOptOutViewModel model = await moderatorService
					.GetViewForOptingOut(moderatorId);
				
				return View(model);
			}
			catch (Exception)
			{
				return GeneralError();
			}

		}

		//[HttpPost]
		//public async Task<IActionResult> OptOut(ModeratorOptOutViewModel model)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		string userId = User.GetId()!;
		//		string moderatorId = await moderatorService.GetModeratorIdByUserIdAsync(userId);

		//		try
		//		{
		//			var moderator = await _context.Moderators.FindAsync(moderatorId);
		//			if (moderator != null)
		//			{
		//				// Validate password (using your password hashing logic)
		//				if (await _userManager.CheckPasswordAsync(moderator, model.Password))
		//				{
		//					moderator.IsApproved = false;
		//					_context.Update(moderator);
		//					await _context.SaveChangesAsync();
		//					return RedirectToAction("Confirmation", "Moderator"); // Redirect to confirmation view
		//				}
		//				else
		//				{
		//					ModelState.AddModelError(string.Empty, "Invalid password!");
		//				}
		//			}
		//			else
		//			{
		//				ModelState.AddModelError(string.Empty, "Moderator not found!");
		//			}
		//		}
		//		catch (Exception)
		//		{
		//			return GeneralError(); // Handle general error
		//		}
		//	}

		//	// Re-populate view model with existing values (optional)
		//	model.PhoneNumber = await moderatorService.GetPhoneNumberByModeratorIdAsync(moderatorId); // Assuming you have this method

		//	return View(model); // Return view with validation errors
		//}

		private IActionResult GeneralError()
		{
			TempData[ErrorMessage] =
				"Unexpected error occurred! Please try again later or contact administrator";

			return RedirectToAction("All", "Game");
		}
	}
}
