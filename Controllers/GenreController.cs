using Microsoft.AspNetCore.Mvc;
using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.Infrastructure.Extensions;
using VideoGameLibrary.Web.ViewModels.Genre;

namespace VideoGameLibrary.Controllers
{
    using static VideoGameLibrary.Common.NotificationMessagesConstants;
    public class GenreController : Controller
	{
		private readonly IGenreService genreService;
		private readonly IModeratorService moderatorService;

		public GenreController(IGenreService genreService, IModeratorService moderatorService)
        {
            this.genreService = genreService;
            this.moderatorService = moderatorService;
        }

        //[HttpGet]
        //public async Task<IActionResult> All()
        //{
        //	var viewModel = await genreService.AllGenresAsync();

        //	return View(viewModel);
        //}

        //[HttpGet]
        //public async Task<IActionResult> Details(int id, string information)
        //{
        //    bool categoryExists = await categoryService.ExistsByIdAsync(id);
        //    if (!categoryExists)
        //    {
        //        return NotFound();
        //    }

        //    CategoryDetailsViewModel viewModel =
        //        await categoryService.GetDetailsByIdAsync(id);
        //    if (viewModel.GetUrlInformation() != information)
        //    {
        //        return NotFound();
        //    }

        //    return View(viewModel);
        //}

        [HttpGet]
        public async Task<IActionResult> GenreCrud()
        {
            string user = User.GetId()!;
            bool isUserModerator = await moderatorService.ModeratorExistsByUserIdAsync(user);

            if (!isUserModerator)
            {
                TempData[ErrorMessage] = "You must be a moderator if you want to add a genre!";
                return RedirectToAction("All", "Game");
            }

            NewGenreViewModel model = new NewGenreViewModel();
            model.ExistingGenres = await genreService.AllGenresAsync();

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewGenreViewModel model)
        {
            if (!await ValidateAndCheckModerator())
            {
                // Validation or moderator check failed, return the appropriate view
                model.ExistingGenres = await genreService.AllGenresAsync();
                return RedirectToAction("All", "Game");
            }

            try
            {
                // Add the platform using the provided model
                await genreService.AddGenreAsync(model);
                TempData[SuccessMessage] = "Genre added successfully!";
                return RedirectToAction("GenreCrud", "Genre");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add a new genre!");
                // If an error occurs, return the view with the same model
                model.ExistingGenres = await genreService.AllGenresAsync();
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(NewGenreViewModel model)
        {
            if (!await ValidateAndCheckModerator())
            {
                model.ExistingGenres = await genreService.AllGenresAsync();
                return View(model);
            }

            bool existsById = await genreService.ExistsByIdAsync(model.Id);
            if (!existsById)
            {
                TempData[ErrorMessage] = "Genre doesn't exist!";
                return RedirectToAction("GenreCrud", "Genre");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, NewGenreViewModel model)
        {

            if (!await ValidateAndCheckModerator())
            {
                model.ExistingGenres = await genreService.AllGenresAsync();
                return View(model);
            }

            bool existsById = await genreService.ExistsByIdAsync(id);
            if (!existsById)
            {
                TempData[ErrorMessage] = "Genre doesn't exist!";
                return RedirectToAction("GenreCrud", "Genre");
            }

            try
            {
                await genreService.UpdateGenreByIdAsync(id, model);
                TempData[SuccessMessage] = "Genre edited successfully!";
                return RedirectToAction("GenreCrud", "Genre");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong while trying to edit genre, please try again later";
                return RedirectToAction("GenreCrud", "Genre");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool genreExists = await genreService.ExistsByIdAsync(id);
            
			if (!genreExists)
			{
				return NotFound();
			}
			Genre genre = await genreService.FetchGenreByIdAsync(id);


			GenreDeleteViewModel model = new GenreDeleteViewModel()
            {
                Id = genre.Id,
                Name = genre.Name,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(GenreDeleteViewModel model)
        {
            string user = User.GetId()!;
            bool isUserModerator = await moderatorService.ModeratorExistsByUserIdAsync(user);

			bool genreExists = await genreService.ExistsByIdAsync(model.Id);

			if (!genreExists)
			{
				return NotFound();
			}

			if (!isUserModerator)
            {
                TempData[ErrorMessage] = "You must be a moderator if you want to edit a platform!";
                return RedirectToAction("All", "Game");
            }

            bool existsById = await genreService.ExistsByIdAsync(model.Id);
            if (!existsById)
            {
                TempData[ErrorMessage] = "Genre doesn't exist!";
                return RedirectToAction("GenreCrud", "Genre");
            }

            try
            {
                await genreService.DeleteGenreByIdAsync(model.Id);
                TempData[WarningMessage] = "Genre deleted successfully!";
                return RedirectToAction("GenreCrud", "Genre");
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong while trying to delete genre, please try again later";
                return RedirectToAction("GenreCrud", "Genre");
            }
        }


        private async Task<bool> ValidateAndCheckModerator()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

                return false;
            }

            string user = User.GetId()!;
            bool isUserModerator = await moderatorService.ModeratorExistsByUserIdAsync(user);

            if (!isUserModerator)
            {
                TempData[ErrorMessage] = "You must be a moderator if you want to edit a genre!";
                return false;
            }

            return true;
        }
    }
}
