using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Web.ViewModels.Game;
using VideoGameLibrary.Web.ViewModels.Platform;


namespace VideoGameLibrary.Services.Data.Interfaces
{
    public interface IPlatformService
    {
        Task<IEnumerable<string>> AllPlatformNamesAsync();
        Task<IEnumerable<GamePlatformSelectFormModel>> AllPlatformsAsync();
        Task<bool> ExistsByIdAsync(int id);
        Task AddPlatformAsync(NewPlatformViewModel model);
        Task<Platform> FetchPlatformByIdAsync(int id);
        Task UpdatePlatformByIdAsync(int id, NewPlatformViewModel model);
        Task DeletePlatformByIdAsync(int id);
        Task<NewPlatformViewModel> GetPlatformForUpdateByIdAsync(int id);
	}
}
