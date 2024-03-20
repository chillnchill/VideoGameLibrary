using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.ViewModels.Game;
using VideoGameLibrary.Web.ViewModels.Game.VideoGameLibrary.Web.ViewModels.Game;
using VideoGameLibrary.Web.Infrastructure.Extensions;


namespace VideoGameLibrary.Controllers
{
    using static VideoGameLibrary.Common.NotificationMessagesConstants;

    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameService gameService;
        private readonly IGenreService genreService;
        private readonly IPlatformService platformService;
        public GameController(IGameService gameService, IGenreService genreService, IPlatformService platformService)
        {
            this.gameService = gameService;
            this.genreService = genreService;
            this.platformService = platformService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> AllGames()
        {
            var allGames = await gameService.GetAllGamesAsync();
            return View(allGames);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                GameFormModel model = new GameFormModel()
                {
                    Genre = await genreService.AllGenresAsync(),
                    Platform = await platformService.AllPlatformsAsync(),
                };

                return View(model);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add(GameFormModel model)
		{
            model.OwnerId = User.GetId()!;

            bool platformsExist =
				await platformService.ExistsByIdAsync(model.PlatformId);
			if (!platformsExist)
			{
				// Adding model error to ModelState automatically makes ModelState Invalid
				ModelState.AddModelError(nameof(model.PlatformId), "Selected platform does not exist!");
			}

			bool genresExist =
				await genreService.ExistsByIdAsync(model.GenreId);
			if (!genresExist)
			{
				// Adding model error to ModelState automatically makes ModelState Invalid
				ModelState.AddModelError(nameof(model.GenreId), "Selected genre does not exist!");
			}

            if (!ModelState.IsValid)
            {
                model.Platform = await platformService.AllPlatformsAsync();
                model.Genre = await genreService.AllGenresAsync();


                foreach (var modelStateEntry in ModelState.Values)
                {
                    foreach (var error in modelStateEntry.Errors)
                    {
                        // Log or debug error message
                        Console.WriteLine(error.ErrorMessage);
                    }
                }


                return View(model);
			}

			try
			{
				await gameService.AddGameAsync(model);
				TempData[SuccessMessage] = "Game added successfully!";
                return RedirectToAction("AllGames", "Game");
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add a new game! Please try again later or contact administrator!");
				model.Platform = await platformService.AllPlatformsAsync();
				model.Genre = await genreService.AllGenresAsync();

				return View(model);
			}
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
