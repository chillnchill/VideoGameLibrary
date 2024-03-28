using Microsoft.AspNetCore.Mvc;
using VideoGameLibrary.Services.Data;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.Infrastructure.Extensions;
using VideoGameLibrary.Web.ViewModels.Moderator;

namespace VideoGameLibrary.Controllers
{
    using static VideoGameLibrary.Common.NotificationMessagesConstants;
    public class ModeratorController : Controller
    {
        private readonly IModeratorService moderatorService;

        public ModeratorController(IModeratorService moderatorService)
        {
            this.moderatorService = moderatorService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            string? userId = User.GetId();
            bool isAgent = await moderatorService.ModeratorExistsByUserIdAsync(userId!);
            if (isAgent)
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
                ModelState.AddModelError(nameof(model.PhoneNumber), "Agent with the provided phone number already exists!");
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

    }
}
