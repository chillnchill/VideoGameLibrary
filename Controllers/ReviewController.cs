using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.Infrastructure.Extensions;
using VideoGameLibrary.Web.ViewModels.Review;

namespace VideoGameLibrary.Controllers
{
	using static VideoGameLibrary.Common.NotificationMessagesConstants;

	[Authorize]
	public class ReviewController : Controller
	{
		private readonly IGameService gameService;
		private readonly IReviewService reviewService;

		public ReviewController(IGameService gameService, IReviewService reviewService)
		{
			this.gameService = gameService;
			this.reviewService = reviewService;
		}

		[HttpGet]
		public async Task<IActionResult> Add(string id)
		{
			string userId = User.GetId()!;

			if (userId == null)
			{
				TempData[ErrorMessage] = "You need to be logged in to review a game!";
				return RedirectToAction("All", "Game");
			}

			var gameExists = await gameService.ExistsByIdAsync(id);

			if (!gameExists)
			{
				TempData[ErrorMessage] = "A Game with the provided id does not exist!";

				return RedirectToAction("All", "Game");
			}

			try
			{
				NewReviewViewModel model = new NewReviewViewModel()
				{
					GameId = id,
					OwnerId = userId
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
		public async Task<IActionResult> Add(NewReviewViewModel model)
		{
			string userId = User.GetId()!;

			if (userId == null)
			{
				TempData[ErrorMessage] = "You need to be logged in to review a game!";
				return RedirectToAction("All", "Game");
			}

			model.OwnerId = userId;
			bool gameExists = await gameService.ExistsByIdAsync(model.GameId);

			if (!gameExists)
			{
				TempData[ErrorMessage] = "A Game with the provided id does not exist!";
				return RedirectToAction("All", "Game");
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
				await reviewService.AddReviewAsync(model, userId);
				TempData[SuccessMessage] = "Review added successfully!";
				return RedirectToAction("Details", "Game", new { id = model.GameId });
			}
			catch (Exception)
			{
				ModelState.AddModelError(string.Empty,
					"Unexpected error occurred while trying to review the game. Please try again later or contact administrator!");
				return View(model);
			}
		}


		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			string userId = User.GetId()!;
			if (userId == null)
			{
				TempData[ErrorMessage] = "You need to be logged in to edit a game!";
				return RedirectToAction("All", "Game");
			}

			string gameId = await reviewService.GetGameIdByReviewIdAsync(id);
			bool gameExists = await gameService.ExistsByIdAsync(gameId);
			if (!gameExists)
			{
				TempData[ErrorMessage] = "A Game with the provided id does not exist!";
				return RedirectToAction("All", "Game");
			}


			bool reviewExists = await reviewService.ReviewExistsByIdAsync(id);
			if (!reviewExists)
			{
				TempData[ErrorMessage] = "A review with the provided id does not exist!";
				return RedirectToAction("Details", "Game");
			}

			try
			{
				NewReviewViewModel model = await reviewService.GetReviewForEditAsync(id);
				model.OwnerId = userId;
				model.GameId = gameId;
				return View(model);
			}
			catch (Exception)
			{
				return GeneralError();
			}
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(NewReviewViewModel model)
		{
			string userId = User.GetId()!;
			if (userId == null)
			{
				TempData[ErrorMessage] = "You need to be logged in to edit a game!";
				return RedirectToAction("All", "Game");
			}

			bool exists = await reviewService.ReviewExistsByIdAsync(model.Id);
			if (!exists)
			{
				TempData[ErrorMessage] = "A review with the provided id does not exist!";
				return RedirectToAction("Details", "Game");
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
				await reviewService.EditReviewAsync(model, model.Id);
				TempData[SuccessMessage] = "Review edited successfully!";
				return RedirectToAction("Details", "Game", new { id = model.GameId });
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

			return RedirectToAction("Add", "Review");
		}
	}
}
