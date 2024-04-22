using Microsoft.AspNetCore.Mvc;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.Infrastructure.Extensions;

namespace VideoGameLibrary.Areas.Admin.Controllers
{
    using static VideoGameLibrary.Common.NotificationMessagesConstants;
    public class GameController : BaseAdminController
	{
        private IGameService gameService;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> DeletedGames()
		{
            bool isUserAdmin = User.IsAdmin();
           
            if (!isUserAdmin)
            {
                TempData[ErrorMessage] = "You have no access to this resource";
                return RedirectToAction("All", "Game");
            }

            try
            {
                var model = await gameService.AllDeletedGamesAsync();
                return View(model);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Restore(string id)
        {
            bool isUserAdmin = User.IsAdmin();
            
            if (!isUserAdmin)
            {
                TempData[ErrorMessage] = "You have no access to this resource";
                return RedirectToAction("All", "Game");
            }

            bool gameExists = await gameService.ExistsByIdAsync(id);
            if (!gameExists)
            {
                TempData[ErrorMessage] = "Game with the provided id does not exist!";
                return RedirectToAction("All", "Game");
            }

            try
            {
                await gameService.RestoreDeletedAsync(id);
                TempData[SuccessMessage] = "Game restored successfully!";
                return RedirectToAction("All", "Game", new { Area = "" });
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
