using Microsoft.EntityFrameworkCore;
using VideoGameLibrary.Data;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Services.Data;
using VideoGameLibrary.Web.ViewModels.Platform;
using VideoGameLibrary.Data.Models;

namespace VideoGameLibrary.Services.Tests
{
	using static DbSeeder;
	public class PlatformServiceTests
	{
		private DbContextOptions<VideoGameLibraryDbContext> dbOptions;
		private VideoGameLibraryDbContext dbContext;

		private IPlatformService platformService;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			dbOptions = new DbContextOptionsBuilder<VideoGameLibraryDbContext>()
				.UseInMemoryDatabase("VideoGameLibraryInMemory")
				.Options;
			dbContext = new VideoGameLibraryDbContext(dbOptions);

			dbContext.Database.EnsureCreated();
			SeedPlatforms(dbContext);

			platformService = new PlatformService(dbContext);
		}

		[Test]
		public async Task ExistsByIdAsync_ReturnsTrue_WhenPlatformExists()
		{
			int existingPlatformId = 1;
			bool result = await platformService.ExistsByIdAsync(existingPlatformId);

			Assert.IsTrue(result);
		}

		[Test]
		public async Task ExistsByIdAsync_ReturnsFalse_WhenPlatformDoesNotExist()
		{
			int nonExistingPlatformId = 999;
			bool result = await platformService.ExistsByIdAsync(nonExistingPlatformId);

			Assert.IsFalse(result);
		}

		[Test]
		public async Task FetchPlatformByIdAsync_ReturnsCorrectPlatform_WhenPlatformExists()
		{
			int existingPlatformId = 1;

			var platform = await platformService.FetchPlatformByIdAsync(existingPlatformId);

			Assert.IsNotNull(platform);
			Assert.AreEqual(existingPlatformId, platform.Id);
		}

		[Test]
		public async Task FetchPlatformByIdAsync_ReturnsNull_WhenPlatformDoesNotExist()
		{
			int nonExistingPlatformId = 999;

			Assert.ThrowsAsync<InvalidOperationException>(async () =>
			{
				var platform = await platformService.FetchPlatformByIdAsync(nonExistingPlatformId);
			});
		}

		[Test]
		public async Task AddPlatformAsync_CreatesNewPlatform()
		{
			var newPlatformViewModel = new NewPlatformViewModel
			{
				NewPlatformName = "Test Platform"
			};

			await platformService.AddPlatformAsync(newPlatformViewModel);

			var createdPlatform = await dbContext.Platforms.FirstOrDefaultAsync(p => p.Name == newPlatformViewModel.NewPlatformName);
			Assert.IsNotNull(createdPlatform);
			Assert.AreEqual(newPlatformViewModel.NewPlatformName, createdPlatform.Name);
		}

		[Test]
		public async Task DeletePlatformByIdAsync_DeletesExistingPlatform()
		{
			var newPlatform = new Platform { Name = "Test Platform" };
			dbContext.Platforms.Add(newPlatform);
			await dbContext.SaveChangesAsync();

			await platformService.DeletePlatformByIdAsync(newPlatform.Id);

			var deletedPlatform = await dbContext.Platforms.FindAsync(newPlatform.Id);
			Assert.IsNull(deletedPlatform);
		}

		[Test]
		public async Task UpdatePlatformByIdAsync_UpdatesExistingPlatform()
		{
			var existingPlatform = new Platform { Name = "Original Platform Name" };
			dbContext.Platforms.Add(existingPlatform);
			await dbContext.SaveChangesAsync();

			var updatedPlatformName = "Updated Platform Name";

			await platformService.UpdatePlatformByIdAsync(existingPlatform.Id, new NewPlatformViewModel { NewPlatformName = updatedPlatformName });

			var updatedPlatform = await dbContext.Platforms.FindAsync(existingPlatform.Id);
			Assert.NotNull(updatedPlatform);
			Assert.AreEqual(updatedPlatformName, updatedPlatform.Name);
		}

	}
}
