using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VideoGameLibrary.Data;
using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Services.Data.Models.Game;
using VideoGameLibrary.Web.ViewModels.Game;
using VideoGameLibrary.Web.ViewModels.Game.Enums;
using VideoGameLibrary.Web.ViewModels.Game.VideoGameLibrary.Web.ViewModels.Game;



namespace VideoGameLibrary.Services.Data
{
    public class GameService : IGameService
	{
		private readonly VideoGameLibraryDbContext dbContext;

		public GameService(VideoGameLibraryDbContext dbContext)
		{
			this.dbContext = dbContext;
		}


		public async Task<IEnumerable<AllGamesViewModel>> AllLikedGamesAsync(string userId)
		{
			Game[] games = await dbContext
				.Users
				.Where(u => u.Id.ToString() == userId)
				.Include(u => u.LikedGames)
				.SelectMany(u => u.LikedGames)
				.ToArrayAsync();

			return games.Select(game => MapGameToViewModel(game));
		}

		public async Task<IEnumerable<AllGamesViewModel>> AllAddedGamesByUserAsync(string userId)
		{
			Game[] games = await dbContext
				.Users
				.Where(u => u.Id.ToString() == userId)
				.Include(u => u.AddedGames)
				.SelectMany(u => u.AddedGames)
				.ToArrayAsync();

			return games.Select(game => MapGameToViewModel(game));
		}

        public async Task<IEnumerable<AllGamesViewModel>> AllDeletedGamesAsync()
        {
            AllGamesViewModel[] games = await dbContext
                .Games
				.Where(game => game.IsDeleted == true)
                .Select(game => MapGameToViewModel(game))
                .ToArrayAsync();

            return games;
        }

        public async Task<GameFormModel> GetGameForEditByIdAsync(string gameId)
		{
			Game model = await dbContext
				.Games
				.FirstAsync(g => g.Id.ToString() == gameId && g.IsDeleted == false);

			return new GameFormModel
			{
				Title = model.Title,
				Description = model.Description,
				Developer = model.Developer,
				Publisher = model.Publisher,
				CoverImg = model.CoverImg,
				Price = model.Price,
				GenreId = model.GenreId,
				PlatformId = model.PlatformId,
				ReleaseDate = model.ReleaseDate,
				Rating = model.Rating,
				NumberOfStars = model.NumberOfStars.ToString()
			};
		}

		public async Task EditGameByIdAsync(GameFormModel model, string gameId)
		{
			Game game = await dbContext
				.Games
				.FirstAsync(g => g.Id.ToString() == gameId);

			game.Title = model.Title;
			game.Description = model.Description;
			game.Developer = model.Developer;
			game.Publisher = model.Publisher;
			game.CoverImg = model.CoverImg;
			game.Price = model.Price;
			game.GenreId = model.GenreId;
			game.PlatformId = model.PlatformId;
			game.ReleaseDate = model.ReleaseDate;
			game.Rating = model.Rating;
			game.NumberOfStars = double.Parse(model.NumberOfStars!);

			await dbContext.SaveChangesAsync();
		}

		public async Task<string> AddGameAsync(GameFormModel model, string userId)
		{
			Game newGame = new Game()
			{
				Title = model.Title,
				Description = model.Description,
				Developer = model.Developer,
				Publisher = model.Publisher,
				CoverImg = model.CoverImg,
				Price = model.Price,
				GenreId = model.GenreId,
				PlatformId = model.PlatformId,
				ReleaseDate = model.ReleaseDate,
				Rating = model.Rating,
				NumberOfStars = double.Parse(model.NumberOfStars!),
				OwnerId = Guid.Parse(userId),
			};

			await dbContext.Games.AddAsync(newGame);
			await dbContext.SaveChangesAsync();

			return newGame.Id.ToString();
		}

		public async Task<bool> ExistsByIdAsync(string gameId)
		{
			bool result = await dbContext.Games
				.AnyAsync(g => g.Id.ToString() == gameId);

			return result;
		}

		public async Task<IEnumerable<AllGamesViewModel>> GetAllGamesAsync()
		{
			AllGamesViewModel[] games = await dbContext
				.Games
				.Where(game => game.IsDeleted == false)
				.Select(game => MapGameToViewModel(game))
				.ToArrayAsync();

			return games;
		}

		public async Task<GameDetailsViewModel> GetGameDetailsByIdAsync(string gameId)
		{
			var game = await dbContext.Games
				.Where(g => g.Id.ToString() == gameId && g.IsDeleted == false)
				.Select(game => new GameDetailsViewModel()
				{
					Id = game.Id.ToString(),
					Title = game.Title,
					Description = game.Description,
					CoverImg = game.CoverImg,
					ReleaseDate = game.ReleaseDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
					Price = game.Price,
					NumberOfStars = game.NumberOfStars,
					Genre = game.Genre.Name,
					Platform = game.Platform.Name,
					Rating = game.Rating,
				})
				.FirstOrDefaultAsync();

			return game;

		}

		public async Task<bool> IsOwnerWithIdCreatorOfGameWithId(string gameId, string ownerId)
		{
			var game = await dbContext.Games
				 .Where(g => g.Id.ToString() == gameId)
				 .FirstAsync();

			if (game.OwnerId.ToString() == ownerId)
			{
				return true;
			}

			return false;
		}

		public async Task<DeleteViewModel> GetGameForDeletionAsync(string gameId)
		{
			var gameForDeletion = await dbContext.Games
			   .Where(g => g.Id.ToString() == gameId && g.IsDeleted == false)
			   .Select(g => new DeleteViewModel()
			   {
				   Id = g.Id.ToString(),
				   Title = g.Title,
				   Description = g.Description,
				   Platform = g.Platform.Name
			   })
			   .FirstAsync();

			return gameForDeletion;
		}

		public async Task DeleteGameAsync(string gameId)
		{
			Game game = await dbContext.Games
			   .Where(g => g.Id.ToString() == gameId)
			   .FirstAsync();

			game.IsDeleted = true;
			await dbContext.SaveChangesAsync();
		}

        public async Task RestoreDeletedAsync(string gameId)
        {
            Game game = await dbContext.Games
                .Where(g => g.Id.ToString() == gameId)
                .FirstAsync();

            game.IsDeleted = false;
            await dbContext.SaveChangesAsync();
        }

        public async Task<AllGamesFilteredAndSortingModel> GetAllGamesSortingAsync(AllGamesQueryModel queryModel)
		{
			IQueryable<Game> gamesQuery = dbContext
			   .Games
			   .AsQueryable();

			if (!string.IsNullOrWhiteSpace(queryModel.Genre))
			{
				gamesQuery = gamesQuery
					.Where(h => h.Genre.Name == queryModel.Genre);
			}

			if (!string.IsNullOrWhiteSpace(queryModel.Platform))
			{
				gamesQuery = gamesQuery
					.Where(h => h.Platform.Name == queryModel.Platform);
			}

			//based on SQL wildcards, string may(not) exist
			if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
			{
				string wildCard = $"%{queryModel.SearchString.ToLower()}%";

				gamesQuery = gamesQuery
					.Where(h => EF.Functions.Like(h.Title, wildCard) ||
								EF.Functions.Like(h.Description, wildCard));
			}

			gamesQuery = queryModel.GameSorting switch
			{
				GameSorting.ReleaseDateAscending => gamesQuery
					.OrderByDescending(g => g.ReleaseDate),
				GameSorting.ReleaseDateDescending => gamesQuery
					.OrderBy(g => g.ReleaseDate),
				GameSorting.PriceDescending => gamesQuery
					.OrderByDescending(g => g.Price),
				GameSorting.PriceAscending => gamesQuery
					.OrderBy(g => g.Price),
				GameSorting.NumberOfStarsDescending => gamesQuery
				.OrderByDescending(g => g.NumberOfStars),
				GameSorting.NumberOfStarsAscending => gamesQuery
					.OrderBy(g => g.NumberOfStars),
				_ => gamesQuery
					.OrderBy(g => g.NumberOfStars)
			};


			//we're skipping the page we are at -1 and multiply by the amount of games per page
			IEnumerable<AllGamesViewModel> allGames = await gamesQuery
                .Where(game => game.IsDeleted == false)
                .Skip((queryModel.CurrentPage - 1) * queryModel.GamesPerPage)
				.Take(queryModel.GamesPerPage)	
				.Select(game => new AllGamesViewModel
				{
					Id = game.Id.ToString(),
					Title = game.Title,
					CoverImg = game.CoverImg,
					Price = game.Price,
					Developer = game.Developer,
					Publisher = game.Publisher,
					Genre = game.Genre.Name,
					ReleaseDate = game.ReleaseDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
				})
				.ToArrayAsync();

			int totalGames = gamesQuery.Count();

			return new AllGamesFilteredAndSortingModel()
			{
				TotalGamesCount = totalGames,
				Games = allGames
			};
		}
		private static AllGamesViewModel MapGameToViewModel(Game game)
		{
			return new AllGamesViewModel()
			{
				Id = game.Id.ToString(),
				Title = game.Title,
				CoverImg = game.CoverImg,
				Price = game.Price,
				Developer = game.Developer,
				Publisher = game.Publisher,
				Genre = game.Genre?.Name,
				ReleaseDate = game.ReleaseDate.ToString("dd/M/yyyy", CultureInfo.InvariantCulture),
			};
		}

       
    }
}
