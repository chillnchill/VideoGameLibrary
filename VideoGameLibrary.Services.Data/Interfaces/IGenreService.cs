using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Web.ViewModels.Game;
using VideoGameLibrary.Web.ViewModels.Genre;
namespace VideoGameLibrary.Services.Data.Interfaces
{
	public interface IGenreService
	{
		Task<IEnumerable<string>> AllGenreNamesAsync();
		Task<IEnumerable<GameGenreSelectFormModel>> AllGenresAsync();
		Task<bool> ExistsByIdAsync(int id);
		Task AddGenreAsync(NewGenreViewModel model);
		Task<Genre> FetchGenreByIdAsync(int id);
		Task UpdateGenreByIdAsync(int id, NewGenreViewModel model);
		Task DeleteGenreByIdAsync(int id);
		Task<NewGenreViewModel> GetGenreForUpdateByIdAsync(int id);
	}
}
