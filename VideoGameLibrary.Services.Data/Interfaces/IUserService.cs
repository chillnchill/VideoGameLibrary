using Microsoft.AspNetCore.Identity;
using VideoGameLibrary.Web.ViewModels.User;

namespace VideoGameLibrary.Services.Data.Interfaces
{
	public interface IUserService
	{
		Task AddLikedGameAsync(string gameId, string userId);
		Task UnlikeGameAsync(string userId, string gameId);
		Task<string> GetUserNameById(string userId);
		Task<string> GetNicknameByEmailAsync(string email);
		Task<IEnumerable<UserViewModel>> AllAsync();
		Task<bool> IsGameInLikedListAsync(string gameId, string userId);

		Task<bool> GetIsSubmittedByEmailAsync(string email);
	}
}
