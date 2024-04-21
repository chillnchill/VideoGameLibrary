using Microsoft.EntityFrameworkCore;
using VideoGameLibrary.Data;
using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.ViewModels.Game;
using VideoGameLibrary.Web.ViewModels.Platform;

namespace VideoGameLibrary.Services.Data
{

	public class PlatformService : IPlatformService
	{
		private readonly VideoGameLibraryDbContext dbContext;
		public PlatformService(VideoGameLibraryDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<IEnumerable<string>> AllPlatformNamesAsync()
		{
			IEnumerable<string> names = await dbContext
				.Platforms
				.Select(p => p.Name)
				.ToArrayAsync();

			return names;
		}

		public async Task AddPlatformAsync(NewPlatformViewModel model)
		{
			Platform platform = new Platform()
			{
				Name = model.NewPlatformName
			};

			await dbContext.Platforms.AddAsync(platform);
			await dbContext.SaveChangesAsync();
		}

		public async Task UpdatePlatformByIdAsync(int id, NewPlatformViewModel model)
		{
			Platform platform = await dbContext
				.Platforms
				.Where(p => p.Id == id)
				.FirstAsync();

			platform.Name = model.NewPlatformName;
			await dbContext.SaveChangesAsync();
		}


		public async Task<Platform> FetchPlatformByIdAsync(int id)
		{
			Platform platform = await dbContext
				.Platforms
				.Where(p => p.Id == id)
				.FirstAsync();

			return platform;
		}

		public async Task<NewPlatformViewModel> GetPlatformForUpdateByIdAsync(int id)
		{
			NewPlatformViewModel platformForUpdate = await dbContext
			   .Platforms
			   .Where(g => g.Id == id)
			   .Select(g => new NewPlatformViewModel
			   {
				   Id = g.Id,
				   NewPlatformName = g.Name,
			   })
			   .FirstAsync();

			return platformForUpdate;
		}

		public async Task<IEnumerable<GamePlatformSelectFormModel>> AllPlatformsAsync()
		{
			IEnumerable<GamePlatformSelectFormModel> allPlatforms = await dbContext
				.Platforms
				.AsNoTracking()
				.Select(c => new GamePlatformSelectFormModel()
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToArrayAsync();

			return allPlatforms;
		}

		public async Task<bool> ExistsByIdAsync(int id)
		{
			bool result = await dbContext
				.Platforms
				.AnyAsync(c => c.Id == id);

			return result;
		}

		public async Task DeletePlatformByIdAsync(int id)
		{
			Platform platform = await dbContext
				.Platforms
				.Where(p => p.Id == id)
				.FirstAsync();

			dbContext.Remove(platform);
			await dbContext.SaveChangesAsync();
		}
	}
}
