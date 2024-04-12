using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.Infrastructure.Extensions;
using VideoGameLibrary.Web.ViewModels.Platform;

namespace VideoGameLibrary.Controllers
{
	using static VideoGameLibrary.Common.NotificationMessagesConstants;

	[Authorize]
	public class PlatformController : Controller
	{
		private readonly IPlatformService platformService;
		private readonly IModeratorService moderatorService;

		public PlatformController(IPlatformService platformService, IModeratorService moderatorService)
		{
			this.platformService = platformService;
			this.moderatorService = moderatorService;
		}

		[HttpGet]
		public async Task<IActionResult> PlatformCrud()
		{
			if (!User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be an administrator if you want to add a platform!";
				return RedirectToAction("All", "Game");
			}

			NewPlatformViewModel model = new NewPlatformViewModel();
			model.ExistingPlatforms = await platformService.AllPlatformsAsync();

			return View(model);

		}

		[HttpPost]
		public async Task<IActionResult> Create(NewPlatformViewModel model)
		{
			if (!User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be an administrator if you want add a platform!";
				return RedirectToAction("All", "Game");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				// Add the platform using the provided model
				await platformService.AddPlatformAsync(model);
				TempData[SuccessMessage] = "Platform added successfully!";
				return RedirectToAction("PlatformCrud", "Platform");
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add a new platform!");
				// If an error occurs, return the view with the same model
				model.ExistingPlatforms = await platformService.AllPlatformsAsync();
				return View(model);
			}
		}

		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			if (!User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be an administrator if you want edit a platform!";
				return RedirectToAction("All", "Game");
			}

			bool existsById = await platformService.ExistsByIdAsync(id);
			if (!existsById)
			{
				TempData[ErrorMessage] = "Platform doesn't exist!";
				return RedirectToAction("PlatformCrud", "Platform");
			}

			try
			{
				NewPlatformViewModel model = await platformService.GetPlatformForUpdateByIdAsync(id);
				return View(model);
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Something went wrong while trying to edit the platform, please try again later";
				return RedirectToAction("GenreCrud", "Genre");
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(int id, NewPlatformViewModel model)
		{

			if (!User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be an administrator if you want edit a platform!";
				return RedirectToAction("All", "Game");
			}

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			bool existsById = await platformService.ExistsByIdAsync(id);
			if (!existsById)
			{
				TempData[ErrorMessage] = "Platform doesn't exist!";
				return RedirectToAction("PlatformCrud", "Platform");
			}

			try
			{
				await platformService.UpdatePlatformByIdAsync(id, model);
				TempData[SuccessMessage] = "Platform edited successfully!";
				return RedirectToAction("PlatformCrud", "Platform");
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Something went wrong while trying to edit the platform";
				return RedirectToAction("PlatformCrud", "Platform");
			}
		}


		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			if (!User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be an administrator if you want to delete a platform!";
				return RedirectToAction("All", "Game");
			}

			bool platformExists = await platformService.ExistsByIdAsync(id);
			if (!platformExists)
			{
				return NotFound();
			}

			var platform = await platformService.FetchPlatformByIdAsync(id);

			PlatformDeleteViewModel model = new PlatformDeleteViewModel()
			{
				Id = platform.Id,
				Name = platform.Name,
			};

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(PlatformDeleteViewModel model)
		{
			if (!User.IsAdmin())
			{
				TempData[ErrorMessage] = "You must be an administrator if you want to delete a platform!";
				return RedirectToAction("All", "Game");
			}

			bool existsById = await platformService.ExistsByIdAsync(model.Id);
			if (!existsById)
			{
				TempData[ErrorMessage] = "Platform doesn't exist!";
				return RedirectToAction("PlatformCrud", "Platform");
			}

			try
			{
				await platformService.DeletePlatformByIdAsync(model.Id);
				TempData[WarningMessage] = "Platform deleted successfully!";
				return RedirectToAction("PlatformCrud", "Platform");
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Something went wrong, please try again later";
				return RedirectToAction("PlatformCrud", "Platform");
			}
		}
	}
}
