using Microsoft.EntityFrameworkCore;
using VideoGameLibrary.Data;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Services.Data;
using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Web.ViewModels.Review;



namespace VideoGameLibrary.Tests
{
	using static DbSeeder;
	public class ReviewServiceTests
	{
		private DbContextOptions<VideoGameLibraryDbContext> dbOptions;
		private VideoGameLibraryDbContext dbContext;

		private IReviewService reviewService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			dbOptions = new DbContextOptionsBuilder<VideoGameLibraryDbContext>()
				.UseInMemoryDatabase("VideoGameLibraryInMemory")
				.Options;
			dbContext = new VideoGameLibraryDbContext(dbOptions);

			dbContext.Database.EnsureCreated();
			SeedReviews(dbContext);

			IUserService userService = new UserService(dbContext);
			reviewService = new ReviewService(dbContext, userService);


		}

		[Test]
		public async Task ReviewExistsByIdAsync_ReturnsTrue_WhenReviewExists()
		{

			var existingReviewId = dbContext.Reviews.First().Id.ToString();
			var result = await reviewService.ReviewExistsByIdAsync(existingReviewId);
			Assert.IsTrue(result);
		}

		[Test]
		public async Task ReviewExistsByIdAsync_ReturnsFalse_WhenReviewDoesNotExist()
		{
			var nonExistingReviewId = Guid.NewGuid().ToString();

			var result = await reviewService.ReviewExistsByIdAsync(nonExistingReviewId);

			Assert.IsFalse(result);
		}

		[Test, Order(1)]
		public async Task GetReviewForEditAsync_ReturnsCorrectReview_WhenReviewExists()
		{
			Review seededReview = await dbContext.Reviews.FirstOrDefaultAsync();
			string seededReviewId = seededReview!.Id.ToString();
			var expectedContent = "Great game, highly recommended!";
			var expectedRating = 5;

			var result = await reviewService.GetReviewForEditAsync(seededReviewId);

			Assert.IsNotNull(result);
			Assert.AreEqual(expectedContent, result.Content);
			Assert.AreEqual(expectedRating, result.Rating);
		}

		[Test]
		public async Task EditReviewAsync_UpdatesReview_WhenReviewExists()
		{
			Review seededReview = await dbContext.Reviews.FirstOrDefaultAsync();
			string seededReviewId = seededReview?.Id.ToString();
			var updatedContent = "Updated review content";
			var updatedRating = 4;

			await reviewService.EditReviewAsync(new NewReviewViewModel { Content = updatedContent, Rating = updatedRating }, seededReviewId);

			Review editedReview = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Id.ToString() == seededReviewId);
			Assert.IsNotNull(editedReview);
			Assert.AreEqual(updatedContent, editedReview.Content);
			Assert.AreEqual(updatedRating, editedReview.Rating);
		}

		[Test]
		public async Task DeleteReviewByIdAsync_DeletesReview_WhenReviewExists()
		{
			Review seededReview = await dbContext.Reviews.FirstOrDefaultAsync();
			string seededReviewId = seededReview?.Id.ToString();

			await reviewService.DeleteReviewByIdAsync(seededReviewId);

			Review deletedReview = await dbContext.Reviews.FirstOrDefaultAsync(r => r.Id.ToString() == seededReviewId);
			Assert.IsNull(deletedReview);
		}

		[Test]
		public async Task GetGameIdByReviewIdAsync_ReturnsCorrectGameId_WhenReviewExists()
		{
			Review seededReview = await dbContext.Reviews.FirstOrDefaultAsync();
			string seededReviewId = seededReview?.Id.ToString();
			string expectedGameId = seededReview?.GameId.ToString();

			string result = await reviewService.GetGameIdByReviewIdAsync(seededReviewId);

			Assert.AreEqual(expectedGameId, result);
		}


	}
}
