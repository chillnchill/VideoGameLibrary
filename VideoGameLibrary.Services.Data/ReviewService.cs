using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VideoGameLibrary.Data;
using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.ViewModels.Review;


namespace VideoGameLibrary.Services.Data
{
	public class ReviewService : IReviewService
	{
		private readonly VideoGameLibraryDbContext dbContext;
		private readonly IUserService userService;
		public ReviewService(VideoGameLibraryDbContext dbContext, IUserService userService)
		{
			this.dbContext = dbContext;
			this.userService = userService;
		}
		public async Task AddReviewAsync(NewReviewViewModel model, string userId)
		{
			Review review = new Review();
			review.Id = Guid.Parse(model.Id);
			review.Content = model.Content;
			review.Rating = model.Rating;
			review.GameId = Guid.Parse(model.GameId);
			review.OwnerId = Guid.Parse(model.OwnerId);

			await dbContext.Reviews.AddAsync(review);
			await dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<AllReviewsViewModel>> AllReviewsPerGameAsync(string gameId)
		{
			IQueryable<Review> allReviews = dbContext.Reviews
				 .Where(r => r.GameId.ToString() == gameId);

			var reviews = await allReviews.ToArrayAsync();

			var reviewViewModels = new List<AllReviewsViewModel>();

			foreach (var review in reviews)
			{
				
				string email = await userService.GetUserNameById(review.OwnerId.ToString());
				string nickName = await userService.GetNicknameByEmailAsync(email);

				reviewViewModels.Add(new AllReviewsViewModel
				{
					Id = review.Id.ToString(),
					Author = nickName,
					Content = review.Content,
					Rating = review.Rating,
					Date = review.DatePosted.ToString("dd/M/yyyy HH:mm", CultureInfo.InvariantCulture)
				});
			}

			return reviewViewModels;
		}

		public async Task DeleteReviewByIdAsync(string id)
		{
			Review reviewForDeletion = await dbContext
				.Reviews
				.Where(r => r.Id.ToString() == id)
				.FirstAsync();

			dbContext.Remove(reviewForDeletion);
			await dbContext.SaveChangesAsync();
		}


		public async Task<bool> ReviewExistsByIdAsync(string id)
		{
			bool result = await dbContext
				.Reviews
				.Where(r => r.Id.ToString() == id)
				.AnyAsync();

			return result;
		}

		public async Task<NewReviewViewModel> GetReviewForEditAsync(string id)
		{
			Review reviewForEdit = await dbContext
				.Reviews
				.Where(r => r.Id.ToString() == id)
				.FirstAsync();

			NewReviewViewModel model = new NewReviewViewModel();

			model.Content = reviewForEdit.Content;
			model.Rating = reviewForEdit.Rating;

			return model;
		}

		public async Task EditReviewAsync(NewReviewViewModel model, string id)
		{
			Review reviewForEdit = await dbContext
				.Reviews
				.Where(r => r.Id.ToString() == id)
				.FirstAsync();

			reviewForEdit.Content = model.Content;
			reviewForEdit.Rating = model.Rating;

			await dbContext.SaveChangesAsync();
		}

		public async Task<string> GetGameIdByReviewIdAsync(string id)
		{
			Review review = await dbContext
				.Reviews
				.Where(r => r.Id.ToString() == id)
				.FirstAsync();

			return review.GameId.ToString();
		}
	}
}
