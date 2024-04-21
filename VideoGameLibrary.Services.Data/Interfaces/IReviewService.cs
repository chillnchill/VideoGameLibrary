using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Web.ViewModels.Game.VideoGameLibrary.Web.ViewModels.Game;
using VideoGameLibrary.Web.ViewModels.Review;

namespace VideoGameLibrary.Services.Data.Interfaces
{
	public interface IReviewService
	{
		Task<IEnumerable<AllReviewsViewModel>> AllReviewsPerGameAsync(string gameId);
		Task AddReviewAsync(NewReviewViewModel model, string userId);
		Task<bool> ReviewExistsByIdAsync(string id);
		Task<NewReviewViewModel> GetReviewForEditAsync(string id);
		Task EditReviewAsync(NewReviewViewModel model, string id);
		Task DeleteReviewByIdAsync(string id);
		Task<string> GetGameIdByReviewIdAsync(string id);
	}
}
