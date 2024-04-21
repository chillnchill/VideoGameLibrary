using Microsoft.EntityFrameworkCore;
using VideoGameLibrary.Data;
using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.ViewModels.Game;
using VideoGameLibrary.Web.ViewModels.Genre;

namespace VideoGameLibrary.Services.Data
{
	public class GenreService : IGenreService
    {
        private readonly VideoGameLibraryDbContext dbContext;
        public GenreService(VideoGameLibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

		public async Task AddGenreAsync(NewGenreViewModel model)
        {
            Genre genre = new Genre()
            {
                Name = model.NewGenreName
            };

            await dbContext.Genres.AddAsync(genre);
            await dbContext.SaveChangesAsync();
        }

		public async Task<IEnumerable<string>> AllGenreNamesAsync()
		{
			IEnumerable<string> names = await dbContext
				.Genres
				.Select(g => g.Name)
				.ToArrayAsync();

			return names;
		}

		public async Task<IEnumerable<GameGenreSelectFormModel>> AllGenresAsync()
		{
			IEnumerable<GameGenreSelectFormModel> allGenres = await dbContext
			   .Genres
			   .AsNoTracking()
			   .Select(c => new GameGenreSelectFormModel()
			   {
				   Id = c.Id,
				   Name = c.Name,
			   })
			   .ToArrayAsync();

			return allGenres;
		}

        public async Task DeleteGenreByIdAsync(int id)
        {
            Genre genre = await dbContext
               .Genres
               .Where(p => p.Id == id)
               .FirstAsync();

            dbContext.Remove(genre);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(int id)
		{
			bool result = await dbContext
				.Genres
				.AnyAsync(c => c.Id == id);

			return result;
		}

        public async Task<Genre> FetchGenreByIdAsync(int id)
        {
            Genre genre = await dbContext
               .Genres
               .Where(p => p.Id == id)
               .FirstAsync();

            return genre;
        }
        public async Task<NewGenreViewModel> GetGenreForUpdateByIdAsync(int id)
        {
            NewGenreViewModel genreForUpdate = await dbContext
               .Genres
               .Where(g => g.Id == id)
               .Select(g => new NewGenreViewModel
               {
                   Id = g.Id,
                   NewGenreName = g.Name,
               })
               .FirstAsync();

            return genreForUpdate;
        }

        public async Task UpdateGenreByIdAsync(int id, NewGenreViewModel model)
        {
            Genre genre = await dbContext
               .Genres
               .Where(p => p.Id == id)
               .FirstAsync();

            genre.Name = model.NewGenreName;
            await dbContext.SaveChangesAsync();
        }



	}
}
