using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Services.Data.Models.Game;
using VideoGameLibrary.Web.ViewModels.Game;
using VideoGameLibrary.Web.ViewModels.Game.VideoGameLibrary.Web.ViewModels.Game;
using VideoGameLibrary.Web.ViewModels.Review;

namespace VideoGameLibrary.Services.Data.Interfaces
{
    public interface IGameService
    {
        Task<bool> ExistsByIdAsync(string gameId);
        Task<IEnumerable<AllGamesViewModel>> GetAllGamesAsync();
        Task<AllGamesFilteredAndSortingModel> GetAllGamesSortingAsync(AllGamesQueryModel queryModel);
        Task<GameDetailsViewModel> GetGameDetailsByIdAsync(string id);
        Task<string> AddGameAsync(GameFormModel model, string userId);
        Task<bool> IsOwnerWithIdCreatorOfGameWithId(string gameId, string ownerId);
        Task<GameFormModel> GetGameForEditByIdAsync(string gameId);
        Task EditGameByIdAsync(GameFormModel model, string gameId);
        Task<DeleteViewModel> GetGameForDeletionAsync(string gameId);
        Task DeleteGameAsync(string gameId);
        Task<IEnumerable<AllGamesViewModel>> AllAddedGamesByUserAsync(string userId);
        Task<IEnumerable<AllGamesViewModel>> AllLikedGamesAsync(string userId);
        Task<IEnumerable<AllGamesViewModel>> AllDeletedGamesAsync();
        Task RestoreDeletedAsync(string gameId);

    }
}
