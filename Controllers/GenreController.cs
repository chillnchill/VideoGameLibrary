using Microsoft.AspNetCore.Mvc;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.ViewModels.Genre;

namespace VideoGameLibrary.Controllers
{
	public class GenreController : Controller
	{
		private readonly IGenreService genreService;

		public GenreController(IGenreService genreService)
		{
			this.genreService = genreService;
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
	}
}
