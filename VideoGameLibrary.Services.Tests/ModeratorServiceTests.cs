using Microsoft.EntityFrameworkCore;
using VideoGameLibrary.Data;
using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Services.Data;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.ViewModels.Genre;
using VideoGameLibrary.Web.ViewModels.Moderator;

namespace VideoGameLibrary.Tests
{
	using static DbSeeder;
	public class ModeratorServiceTests
	{
		private DbContextOptions<VideoGameLibraryDbContext> dbOptions;
		private VideoGameLibraryDbContext dbContext;

		private IModeratorService moderatorService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			dbOptions = new DbContextOptionsBuilder<VideoGameLibraryDbContext>()
				.UseInMemoryDatabase("VideoGameLibraryInMemory" + Guid.NewGuid().ToString())
				.Options;
			dbContext = new VideoGameLibraryDbContext(dbOptions);

			dbContext.Database.EnsureCreated();
			SeedUsers(dbContext);

			IUserService userService = new UserService(dbContext);
			moderatorService = new ModeratorService(dbContext, userService);
		}

		[Test]
		public async Task ModeratorExistsByUserIdAsync_ShouldReturnTrueWhenExists()
		{
			string existingModUserId = ModeratorUser.Id.ToString();

			bool result = await moderatorService.ModeratorExistsByUserIdAsync(existingModUserId);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task ModeratorExistsByUserIdAsync_ShouldReturnFalseWhenNotExists()
		{
			string existingModUserId = NormalUser.Id.ToString();

			bool result = await moderatorService.ModeratorExistsByUserIdAsync(existingModUserId);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task ModeratorExistsByPhoneNumberAsync_ShouldReturnTrue()
		{
			string existingPhone = Moderator.PhoneNumber.ToString();

			bool result = await moderatorService.ModeratorExistsByPhoneNumberAsync(existingPhone);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task ModeratorExistsByPhoneNumberAsync_ShouldReturnFalse()
		{
			string nonExistingPhone = "+1234567890";

			bool result = await moderatorService.ModeratorExistsByPhoneNumberAsync(nonExistingPhone);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task GetModeratorIdByUserIdAsync_ShouldReturnTrue_WhenModeratorExists()
		{
			string existingUserId = ModeratorUser.Id.ToString();
			string moderatorId = await moderatorService.GetModeratorIdByUserIdAsync(existingUserId);

			Assert.IsTrue(!string.IsNullOrEmpty(moderatorId));
		}

		[Test]
		public async Task GetModeratorIdByUserIdAsync_ShouldReturnFalse_WhenModeratorDoesNotExist()
		{
			string nonExistingUserId = "mumbojumbo";
			string moderatorId = null;
			try
			{
				moderatorId = await moderatorService.GetModeratorIdByUserIdAsync(nonExistingUserId);
			}
			catch (InvalidOperationException)
			{

			}
			Assert.IsNull(moderatorId);
		}

		[Test]
		public async Task CreateModeratorAsync_CreatesNewModeratorInDatabase()
		{
			var model = new ModeratorApplicationViewModel
			{
				Id = Guid.NewGuid().ToString(),
				PhoneNumber = "+123456789",
				AboutMe = "Test about me information",
				IsApproved = true
			};

			await moderatorService.CreateModeratorAsync(model);

			var createdModerator = await dbContext.Moderators.FirstOrDefaultAsync(m => m.UserId == Guid.Parse(model.Id));
			Assert.IsNotNull(createdModerator);
			Assert.AreEqual(model.PhoneNumber, createdModerator.PhoneNumber);
			Assert.AreEqual(model.AboutMe, createdModerator.AboutMe);
			Assert.IsFalse(model.IsSubmitted);
			Assert.IsTrue(model.IsApproved);
		}

		[Test]
		public async Task GetAllModeratorsAsync_ReturnsCorrectViewModels()
		{
			var result = await moderatorService.GetAllModeratorsAsync();

			Assert.IsNotNull(result);
			foreach (var viewModel in result)
			{
				var moderator = dbContext.Moderators
					.Include(m => m.User)
					.FirstOrDefault(m => m.Id == Guid.Parse(viewModel.Id));

				Assert.IsNotNull(moderator);
				Assert.AreEqual(moderator.User.Nickname, viewModel.Nickname);
				Assert.AreEqual(moderator.PhoneNumber, viewModel.PhoneNumber);
				Assert.AreEqual(moderator.AboutMe, viewModel.AboutMe);
			}
		}


	}
}