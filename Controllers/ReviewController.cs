using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameLibrary.Services.Data;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.ViewModels.Game.VideoGameLibrary.Web.ViewModels.Game;
using VideoGameLibrary.Web.ViewModels.Review;

namespace VideoGameLibrary.Controllers
{
	using static VideoGameLibrary.Common.NotificationMessagesConstants;

	[Authorize]
	public class ReviewController : Controller
	{
		private readonly IGameService gameService;
	
		public ReviewController(IGameService gameService)
		{
			this.gameService = gameService;
		}
		[HttpGet]
		public async Task<IActionResult> Add(string id)
		{
			var gameExists = await gameService.ExistsByIdAsync(id);

			if (!gameExists)
			{
				TempData[ErrorMessage] = "A Game with the provided id does not exist!";

				return RedirectToAction("All", "Game");
			}

			try
			{
				NewReviewViewModel model = new NewReviewViewModel();

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
				"Unexpected error occurred! Please try again later or contact administrator";

			return RedirectToAction("Add", "Review");
		}
	}
}
