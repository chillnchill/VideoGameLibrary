using Microsoft.EntityFrameworkCore;
using VideoGameLibrary.Data;
using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Services.Data;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.ViewModels.Genre;

namespace VideoGameLibrary.Tests
{
	using static DbSeeder;
	public class GenreServiceTests
	{
		private DbContextOptions<VideoGameLibraryDbContext> dbOptions;
		private VideoGameLibraryDbContext dbContext;

		private IGenreService genreService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			dbOptions = new DbContextOptionsBuilder<VideoGameLibraryDbContext>()
				.UseInMemoryDatabase("VideoGameLibraryInMemory")
				.Options;
			dbContext = new VideoGameLibraryDbContext(dbOptions);

			dbContext.Database.EnsureCreated();
			SeedGenres(dbContext);

			genreService = new GenreService(dbContext);
		}

		[Test]
		public async Task ExistsByIdAsync_ReturnsTrue_WhenGenreExists()
		{
			int existingGenreId = 1;
			bool result = await genreService.ExistsByIdAsync(existingGenreId);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task ExistsByIdAsync_ReturnsFalse_WhenGenreDoesNotExist()
		{
			int nonExistingGenreId = 999;
			bool result = await genreService.ExistsByIdAsync(nonExistingGenreId);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task FetchGenreByIdAsync_ReturnsCorrectGenre_WhenGenreExists()
		{
			int existingGenreId = 1;

			var genre = await genreService.FetchGenreByIdAsync(existingGenreId);

			Assert.IsNotNull(genre);
			Assert.AreEqual(existingGenreId, genre.Id);
		}

		[Test]
		public async Task FetchGenreByIdAsync_ReturnsNull_WhenGenreDoesNotExist()
		{
			int nonExistingGenreId = 999;

			Assert.ThrowsAsync<InvalidOperationException>(async () =>
			{
				var genre = await genreService.FetchGenreByIdAsync(nonExistingGenreId);
			});
		}

		[Test]
		public async Task AddGenreAsync_CreatesNewGenre()
		{
			var newGenreViewModel = new NewGenreViewModel
			{
				NewGenreName = "Test Genre"
			};

			await genreService.AddGenreAsync(newGenreViewModel);

			var createdGenre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Name == newGenreViewModel.NewGenreName);
			Assert.IsNotNull(createdGenre);
			Assert.AreEqual(newGenreViewModel.NewGenreName, createdGenre.Name);
		}

		[Test]
		public async Task DeleteGenreByIdAsync_DeletesExistingGenre()
		{
			var newGenre = new Genre { Name = "Test Genre" };
			dbContext.Genres.Add(newGenre);
			await dbContext.SaveChangesAsync();

			await genreService.DeleteGenreByIdAsync(newGenre.Id);

			var deletedGenre = await dbContext.Genres.FindAsync(newGenre.Id);
			Assert.IsNull(deletedGenre);
		}

		[Test]
		public async Task UpdateGenreByIdAsync_UpdatesExistingGenre()
		{
			var existingGenre = new Genre { Name = "Original Genre Name" };
			dbContext.Genres.Add(existingGenre);
			await dbContext.SaveChangesAsync();

			var updatedGenreName = "Updated Genre Name";

			await genreService.UpdateGenreByIdAsync(existingGenre.Id, new NewGenreViewModel { NewGenreName = updatedGenreName });

			var updatedGenre = await dbContext.Genres.FindAsync(existingGenre.Id);
			Assert.NotNull(updatedGenre);
			Assert.AreEqual(updatedGenreName, updatedGenre.Name);
		}
	}
}
