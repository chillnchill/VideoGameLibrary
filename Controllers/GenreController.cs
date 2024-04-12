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


        [HttpGet]
        public async Task<IActionResult> GenreCrud()
        {

            if (!User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must be an administrator if you want to add a genre!";
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
            if (!User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must be an administrator if you want to add a genre!";
                return RedirectToAction("All", "Game");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
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
        public async Task<IActionResult> Update(int id)
        {
            if (!User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must be an administrator if you want to edit a genre!";
                return RedirectToAction("All", "Game");
            }

            bool existsById = await genreService.ExistsByIdAsync(id);
            if (!existsById)
            {
                TempData[ErrorMessage] = "Genre doesn't exist!";
                return RedirectToAction("GenreCrud", "Genre");
            }

            try
            {
                NewGenreViewModel model = await genreService.GetGenreForUpdateByIdAsync(id);
                return View(model);
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong while trying to edit the genre, please try again later";
                return RedirectToAction("GenreCrud", "Genre");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, NewGenreViewModel model)
        {
            if (!User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must be an administrator if you want to edit a genre!";
                return RedirectToAction("All", "Game");
            }

            if (!ModelState.IsValid)
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
                TempData[ErrorMessage] = "Something went wrong while trying to edit the genre";
                return RedirectToAction("GenreCrud", "Genre");
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must be an administrator if you want to delete a genre!";
                return RedirectToAction("All", "Game");
            }

            bool genreExists = await genreService.ExistsByIdAsync(id);
            if (!genreExists)
            {
                return NotFound();
            }

            //put this in try-catch
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
            if (!User.IsAdmin())
            {
                TempData[ErrorMessage] = "You must be an administrator if you want to delete a genre!";
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
    }
}
