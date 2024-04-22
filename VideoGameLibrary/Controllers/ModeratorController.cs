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
				await moderatorService.Submit(userId!, model);
				TempData[SuccessMessage] = "You've successfully applied to be a moderator!";
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

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Remove()
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
				await moderatorService.RemoveModeratorAsync(moderatorId);
				TempData[SuccessMessage] = "You successfully opted out of being a moderator!";
				return RedirectToAction("All", "Game");
			}
			catch (Exception)
			{
				return GeneralError();
			}

		}

		private IActionResult GeneralError()
		{
			TempData[ErrorMessage] =
				"Unexpected error occurred! Please try again later or contact administrator";

			return RedirectToAction("All", "Game");
		}
	}
}
