using Microsoft.AspNetCore.Mvc;
using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.Infrastructure.Extensions;
using VideoGameLibrary.Web.ViewModels.Moderator;

namespace VideoGameLibrary.Areas.Admin.Controllers
{
    using static VideoGameLibrary.Common.NotificationMessagesConstants;
    public class ModeratorController : BaseAdminController
	{
		private IModeratorService moderatorService;

		public ModeratorController(IModeratorService moderatorService)
		{
			this.moderatorService = moderatorService;
		}

		[HttpGet]
		public async Task<IActionResult> AllSubmissions()
		{
            bool isUserAdmin = User.IsAdmin();

            if (!isUserAdmin)
            {
                TempData[ErrorMessage] = "You have no access to this resource";
                return RedirectToAction("All", "Game");
            }

            try
            {
                var model = await moderatorService.GetModeratorSubmissionsByIdAsync();
                return View(model);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Approve(ModeratorApplicationViewModel model)
		{
            bool isUserAdmin = User.IsAdmin();

            if (!isUserAdmin)
            {
                TempData[ErrorMessage] = "You have no access to this resource";
                return RedirectToAction("All", "Game");
            }

            bool isUserModerator = await moderatorService.ModeratorExistsByUserIdAsync(model.Id!);

			if (isUserModerator)
			{
				TempData[ErrorMessage] = "This user is already a moderator";
				return RedirectToAction("AllSubmissions", "Moderator", new { Area = "Admin" });
			}

			try
			{
                await moderatorService.CreateModeratorAsync(model);
                TempData[SuccessMessage] = "Moderator application approved!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Decline(ModeratorApplicationViewModel model)
        {
			bool isUserAdmin = User.IsAdmin();

			if (!isUserAdmin)
			{
				TempData[ErrorMessage] = "You have no access to this resource";
				return RedirectToAction("All", "Game");
			}

			bool isUserModerator = await moderatorService.ModeratorExistsByUserIdAsync(model.Id!);

			if (isUserModerator)
			{
				TempData[ErrorMessage] = "This user is already a moderator";
				return RedirectToAction("AllSubmissions", "Moderator", new { Area = "Admin" });
			}

			try
			{
				await moderatorService.DeclineSubmissionAsync(model);
				TempData[WarningMessage] = "Moderator application declined!";
				return RedirectToAction("Index", "Home");
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		[HttpGet]
        public async Task<IActionResult> AllModerators()
        {
            bool isUserAdmin = User.IsAdmin();

            if (!isUserAdmin)
            {
                TempData[ErrorMessage] = "You have no access to this resource";
                return RedirectToAction("All", "Game");
            }

            try
            {
                var model = await moderatorService.GetAllModeratorsAsync();
                return View(model);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

		
		private IActionResult GeneralError()
        {
            TempData[ErrorMessage] =
                "Unexpected error occurred! Please try again later";

            return RedirectToAction("Index", "Home", new { Area = "" });
        }

    }
}
