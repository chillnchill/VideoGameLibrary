using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VideoGameLibrary.Data;
using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Services.Data;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Services.Data.Models.Game;
using VideoGameLibrary.Web.ViewModels.Game;
using VideoGameLibrary.Web.ViewModels.Game.Enums;
using VideoGameLibrary.Web.ViewModels.Game.VideoGameLibrary.Web.ViewModels.Game;

namespace VideoGameLibrary.Tests
{
	using static DbSeeder;
	public class GameServiceTests
	{
		private DbContextOptions<VideoGameLibraryDbContext> dbOptions;
		private VideoGameLibraryDbContext dbContext;

		private IGameService gameService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			dbOptions = new DbContextOptionsBuilder<VideoGameLibraryDbContext>()
				.UseInMemoryDatabase("VideoGameLibraryInMemory")
				.Options;
			dbContext = new VideoGameLibraryDbContext(dbOptions);

			dbContext.Database.EnsureCreated();
			SeedGame(dbContext);

			gameService = new GameService(dbContext);
		}

		[Test]
		public async Task ExistsByIdAsync_ReturnsTrue_WhenGameExists()
		{
			string gameId = dbContext.Games.FirstOrDefault()?.Id.ToString();

			bool result = await gameService.ExistsByIdAsync(gameId);

			Assert.IsTrue(result);
		}


		[Test]
		public async Task GetAllGamesSortingAsync_ReturnsCorrectResult()
		{
			AllGamesQueryModel queryModel = new AllGamesQueryModel
			{
				Genre = "Action",
				Platform = "PC",
				SearchString = "adventure",
				GameSorting = GameSorting.PriceDescending,
				CurrentPage = 1,
				GamesPerPage = 3
			};

			AllGamesFilteredAndSortingModel result = await gameService.GetAllGamesSortingAsync(queryModel);

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Games);
			Assert.IsTrue(result.Games.Count() <= queryModel.GamesPerPage);
		}

		[Test, Order(1)]
		public async Task GetGameDetailsByIdAsync_ReturnsCorrectDetails_WhenGameExists()
		{
			Game seededGame = await dbContext.Games.FirstOrDefaultAsync();
			string gameId = seededGame?.Id.ToString();
			string expectedTitle = seededGame?.Title;
			string expectedDescription = seededGame?.Description;

			GameDetailsViewModel result = await gameService.GetGameDetailsByIdAsync(gameId);

			Assert.IsNotNull(result);
			Assert.AreEqual(expectedTitle, result.Title);
			Assert.AreEqual(expectedDescription, result.Description);
		}

		[Test]
		public async Task IsOwnerWithIdCreatorOfGameWithId_ReturnsTrue_WhenOwnerIsCreator()
		{
			Game seededGame = await dbContext.Games.FirstOrDefaultAsync();
			string gameId = seededGame?.Id.ToString();
			string ownerId = seededGame?.OwnerId.ToString();

			bool result = await gameService.IsOwnerWithIdCreatorOfGameWithId(gameId, ownerId);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task IsOwnerWithIdCreatorOfGameWithId_ReturnsFalse_WhenOwnerIsNotCreator()
		{
			Game seededGame = await dbContext.Games.FirstOrDefaultAsync();
			string gameId = seededGame?.Id.ToString();
			string ownerId = "some_other_owner_id";

			bool result = await gameService.IsOwnerWithIdCreatorOfGameWithId(gameId, ownerId);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task DeleteGameAsync_MarksGameAsDeleted()
		{
			Game seededGame = await dbContext.Games.FirstOrDefaultAsync();
			string gameId = seededGame?.Id.ToString();

			await gameService.DeleteGameAsync(gameId);

			Game deletedGame = await dbContext.Games.FirstOrDefaultAsync(g => g.Id.ToString() == gameId);
			Assert.IsNotNull(deletedGame);
			Assert.IsTrue(deletedGame.IsDeleted);
		}

		[Test]
		public async Task RestoreDeletedAsync_RestoresDeletedGame()
		{
			Game seededGame = await dbContext.Games.FirstOrDefaultAsync();
			string gameId = seededGame?.Id.ToString();
			await gameService.DeleteGameAsync(gameId);

			await gameService.RestoreDeletedAsync(gameId);

			Game restoredGame = await dbContext.Games.FirstOrDefaultAsync(g => g.Id.ToString() == gameId);
			Assert.IsNotNull(restoredGame);
			Assert.IsFalse(restoredGame.IsDeleted);
		}

		[Test]
		public async Task AddGameAsync_ReturnsCorrectGameId()
		{
			GameFormModel model = new GameFormModel
			{
				Title = "Test Game",
				Description = "A test game for unit testing",
				Developer = "Test Developer",
				Publisher = "Test Publisher",
				CoverImg = "/images/test-game.jpg",
				Price = 29.99m,
				GenreId = 1,
				PlatformId = 1,
				ReleaseDate = DateTime.UtcNow,
				Rating = "E",
				NumberOfStars = "4.5"
			};

			string userId = "12345678-1234-1234-1234-123456789abc";
			string gameId = await gameService.AddGameAsync(model, userId);

			Assert.IsNotNull(gameId);
			Assert.IsNotEmpty(gameId);

			Game addedGame = await dbContext.Games.FindAsync(Guid.Parse(gameId));
			Assert.IsNotNull(addedGame);
		}

		[Test, Order(2)]
		public async Task GetAllGamesAsync_ReturnsCorrectViewModels()
		{
			var result = await gameService.GetAllGamesAsync();

			Assert.IsNotNull(result);

			Game seededGame = dbContext.Games.FirstOrDefault();


			AllGamesViewModel expectedGameViewModel = new AllGamesViewModel
			{
				Id = seededGame.Id.ToString(),
				Title = seededGame.Title,
				CoverImg = seededGame.CoverImg,
				Price = seededGame.Price,
				Developer = seededGame.Developer,
				Publisher = seededGame.Publisher,
				Genre = seededGame.Genre?.Name,
				ReleaseDate = seededGame.ReleaseDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture)
			};

			Assert.AreEqual(expectedGameViewModel.Id, result.First().Id);
			Assert.AreEqual(expectedGameViewModel.Title, result.First().Title);
			Assert.AreEqual(expectedGameViewModel.CoverImg, result.First().CoverImg);
			Assert.AreEqual(expectedGameViewModel.Price, result.First().Price);
			Assert.AreEqual(expectedGameViewModel.Developer, result.First().Developer);
			Assert.AreEqual(expectedGameViewModel.Publisher, result.First().Publisher);
			Assert.AreEqual(expectedGameViewModel.Genre, result.First().Genre);
			Assert.AreEqual(expectedGameViewModel.ReleaseDate, result.First().ReleaseDate);
		}

	}
}
