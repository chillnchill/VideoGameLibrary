using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Web.ViewModels.Moderator;

namespace VideoGameLibrary.Services.Data.Interfaces
{
    public interface IModeratorService
    {
        Task<bool> ModeratorExistsByUserIdAsync(string userId);
        Task<bool> ModeratorExistsByPhoneNumberAsync(string phoneNumber);
        Task CreateModeratorAsync(ModeratorApplicationViewModel model);
        Task<string> GetModeratorIdByUserIdAsync(string userId);
        Task<ModeratorOptOutViewModel> GetViewForOptingOut(string userId);
        Task Submit(string userId, ModeratorApplicationViewModel model);
        Task<IEnumerable<ModeratorApplicationViewModel>> GetModeratorSubmissionsByIdAsync();
        Task<IEnumerable<ModeratorApplicationViewModel>> GetAllModeratorsAsync();
        Task DeclineSubmissionAsync(ModeratorApplicationViewModel model);
		Task RemoveModeratorAsync(string modId);

	}
}
