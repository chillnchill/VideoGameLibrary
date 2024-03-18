using HouseRentingSystem.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameLibrary.Common;
using VideoGameLibrary.Services.Data;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.ViewModels.Game;

namespace VideoGameLibrary.Controllers
{
    using static VideoGameLibrary.Common.NotificationMessagesConstants;

    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameService gameService;
        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> AllGames()
        {
            var allGames = await gameService.GetAllGamesAsync();
            return View(allGames);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData[ErrorMessage] = "You need to be logged in to view game details!";
                return RedirectToAction("AllGames", "Game");
            }

            // Continue with the rest of your logic
            bool gameExists = await gameService.ExistsByIdAsync(id);
            if (!gameExists)
            {
                TempData[ErrorMessage] = "Game with the provided id does not exist!";
                return RedirectToAction("AllGames", "Game");
            }

            try
            {
                GameDetailsViewModel viewModel = await gameService.GetGameDetailsByIdAsync(id);
                return View(viewModel);
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

            return RedirectToAction("AllGames", "Game");
        }
    }
}
