﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Services.Data.Models.Game;
using VideoGameLibrary.Web.Infrastructure.Extensions;
using VideoGameLibrary.Web.ViewModels.Game;
using VideoGameLibrary.Web.ViewModels.Game.VideoGameLibrary.Web.ViewModels.Game;


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

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> All([FromQuery]AllGamesQueryModel queryModel)
		{
			AllGamesFilteredAndSortingModel model = await gameService.GetAllGamesSortingAsync(queryModel);

			queryModel.Games = model.Games;
			queryModel.TotalGames = model.TotalGamesCount;
			queryModel.Genres = await genreService.AllGenreNamesAsync();
			queryModel.Platforms = await platformService.AllPlatformNamesAsync();

			return View(queryModel);
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

			bool platformsExist =
				await platformService.ExistsByIdAsync(model.PlatformId);
			if (!platformsExist)
			{
				ModelState.AddModelError(nameof(model.PlatformId), "Selected platform does not exist!");
			}

			bool genresExist =
				await genreService.ExistsByIdAsync(model.GenreId);
			if (!genresExist)
			{
				ModelState.AddModelError(nameof(model.GenreId), "Selected genre does not exist!");
			}

			if (!ModelState.IsValid)
			{
				model.Platform = await platformService.AllPlatformsAsync();
				model.Genre = await genreService.AllGenresAsync();

				//var errors = ModelState
				//    .Where(x => x.Value.Errors.Count > 0)
				//    .Select(x => new { x.Key, x.Value.Errors })
				//    .ToArray();


				return View(model);
			}

			try
			{
				string userId = User.GetId()!;
				string gameId = await gameService.AddGameAsync(model, userId);
				TempData[SuccessMessage] = "Game added successfully!";
				return RedirectToAction("Details", "Game", new {id = gameId});
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
		public async Task<IActionResult> Edit(string id)
		{
			var gameExists = await gameService.ExistsByIdAsync(id);

			if (!gameExists)
			{
				TempData[ErrorMessage] = "A Game with the provided id does not exist!";

				return RedirectToAction("All", "Game");
			}

			string userId = User.GetId()!;
			bool isUserOwner = await gameService
			   .IsOwnerWithIdCreatorOfGameWithId(id, userId!);

			if (!isUserOwner)
			{
				TempData[ErrorMessage] = "You must be the game owner if you want to edit!";

				return RedirectToAction("Details", "Game");
			}

			try
			{
				GameFormModel model = await gameService
					.GetGameForEditByIdAsync(id);
				model.Platform = await platformService.AllPlatformsAsync();
				model.Genre = await genreService.AllGenresAsync();

				return View(model);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, GameFormModel model)
		{
			if (!ModelState.IsValid)
			{
				var errors = ModelState
					.Where(x => x.Value.Errors.Count > 0)
					.Select(x => new { x.Key, x.Value.Errors })
					.ToArray();

				model.Platform = await platformService.AllPlatformsAsync();
				model.Genre = await genreService.AllGenresAsync();

				return View(model);
			}

			var gameExists = await gameService.ExistsByIdAsync(id);

			if (!gameExists)
			{
				TempData[ErrorMessage] = "A Game with the provided id does not exist!";

				return RedirectToAction("All", "Game");
			}

			string userId = User.GetId()!;
			bool isUserOwner = await gameService
			   .IsOwnerWithIdCreatorOfGameWithId(id, userId!);

			if (!isUserOwner)
			{
				TempData[ErrorMessage] = "You must be the game owner if you want to edit!";

				return RedirectToAction("All", "Game");
			}

			try
			{
				await gameService.EditGameByIdAsync(model, id);
				
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty,
					"Unexpected error occurred while trying to edit the game. Please try again later or contact administrator!");
				model.Platform = await platformService.AllPlatformsAsync();
				model.Genre = await genreService.AllGenresAsync();

				return View(model);
			}

			TempData[SuccessMessage] = "Game edited successfully!";
			return RedirectToAction("Details", "Game", new { id });
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string id)
		{
			bool gameExists = await gameService.ExistsByIdAsync(id);

			if (!gameExists)
			{
				TempData[ErrorMessage] = "Game with the provided id does not exist!";
				return RedirectToAction("All", "Game");
			}

			string userId = User.GetId()!;
			bool isUserOwner = await gameService
			   .IsOwnerWithIdCreatorOfGameWithId(id, userId!);

			if (!isUserOwner)
			{
				TempData[ErrorMessage] = "You must be the game owner!";

				return RedirectToAction("All", "Game");
			}

			try
			{
				DeleteViewModel model = await gameService
					.GetGameForDeletionAsync(id);
				return View(model);
			}
			catch (Exception)
			{
				return GeneralError();
			}

		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string gameId)
		{
			bool gameExists = await gameService.ExistsByIdAsync(gameId);

			if (!gameExists)
			{
				TempData[ErrorMessage] = "Game with the provided id does not exist!";
				return RedirectToAction("All", "Game");
			}

			string userId = User.GetId()!;
			bool isUserOwner = await gameService
			   .IsOwnerWithIdCreatorOfGameWithId(gameId, userId!);

			if (!isUserOwner)
			{
				TempData[ErrorMessage] = "You must be the game owner!";

				return RedirectToAction("All", "Game");
			}

			try
			{
				await gameService.DeleteGameAsync(gameId);
				TempData[WarningMessage] = "The game was successfully deleted!";
				return RedirectToAction("All", "Game");
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		[HttpGet]
		public async Task<IActionResult> Details(string id)
		{
			if (!User.Identity.IsAuthenticated)
			{
				TempData[ErrorMessage] = "You need to be logged in to view game details!";
				return RedirectToAction("All", "Game");	
			}

			// Continue with the rest of your logic
			bool gameExists = await gameService.ExistsByIdAsync(id);
			if (!gameExists)
			{
				TempData[ErrorMessage] = "Game with the provided id does not exist!";
				return RedirectToAction("All", "Game");
			}

			try
			{
				GameDetailsViewModel viewModel = await gameService.GetGameDetailsByIdAsync(id);

				string user = User.GetId()!;
				var isOwner = await gameService.IsOwnerWithIdCreatorOfGameWithId(id, user);

				if (isOwner)
				{
					viewModel.IsOwner = true;
				}

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

			return RedirectToAction("All", "Game");	
		}
	}
}
