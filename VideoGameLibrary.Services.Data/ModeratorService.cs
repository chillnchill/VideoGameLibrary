using Microsoft.EntityFrameworkCore;
using VideoGameLibrary.Data;
using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.ViewModels.Moderator;


namespace VideoGameLibrary.Services.Data
{
	public class ModeratorService : IModeratorService
	{
		private readonly VideoGameLibraryDbContext dbContext;
		private readonly IUserService userService;

        public ModeratorService(VideoGameLibraryDbContext dbContext, IUserService userService)
		{
			this.dbContext = dbContext;
			this.userService = userService;
		}
        public async Task Submit(string userId, ModeratorApplicationViewModel model)
		{
			ApplicationUser user = await dbContext
				.Users
				.Where(u => u.Id.ToString() == userId)
				.FirstAsync();

			user.IsSubmitted = model.IsSubmitted;
			user.Phone= model.PhoneNumber;
			user.AboutMe = model.AboutMe;

			await dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<ModeratorApplicationViewModel>> GetModeratorSubmissionsByIdAsync()
		{
			ModeratorApplicationViewModel[] submittedApplications = await dbContext
				.Users
				.Where(u => u.IsSubmitted == true) 
				.Select(u => new ModeratorApplicationViewModel
				{
					Id = u.Id.ToString(),
					Nickname = u.Nickname,
					PhoneNumber = u.Phone!, 
					AboutMe = u.AboutMe!, 
				})
				.ToArrayAsync();

			return submittedApplications;
		}

        public async Task<IEnumerable<ModeratorApplicationViewModel>> GetAllModeratorsAsync()
        {
            ModeratorApplicationViewModel[] submittedApplications = await dbContext
                .Moderators
                .Where(m => m.IsApproved == true)
                .Select(m => new ModeratorApplicationViewModel
                {
                    Id = m.Id.ToString(),
                    Nickname = m.User.Nickname,
                    PhoneNumber = m.PhoneNumber,
                    AboutMe = m.AboutMe!
                })
                .ToArrayAsync();

            return submittedApplications;
        }

        public async Task CreateModeratorAsync(ModeratorApplicationViewModel model)
		{
			Moderator newModerator = new Moderator()
			{
				PhoneNumber = model.PhoneNumber,
				AboutMe = model.AboutMe,
				IsApproved = model.IsApproved = true,
				UserId = Guid.Parse(model.Id!) 
			};

			model.IsSubmitted = false;
			model.IsApproved = true;

			await dbContext.Moderators.AddAsync(newModerator);
			await dbContext.SaveChangesAsync();
		}

		public async Task DeclineSubmissionAsync(ModeratorApplicationViewModel model)
		{
			ApplicationUser user = await dbContext
				.Users
				.Where(u => u.Id.ToString() == model.Id)
				.FirstAsync();

			user.IsSubmitted = false;
			model.IsSubmitted = false;
			model.IsApproved = false;

			await dbContext.SaveChangesAsync();
		}

		public async Task<string> GetModeratorIdByUserIdAsync(string userId)
		{
			string result = await dbContext
				.Moderators
				.Where(m => m.UserId.ToString() == userId && m.IsApproved == true)
				.Select(m => m.Id.ToString())
				.FirstAsync();

			return result;
		}

		public async Task<bool> ModeratorExistsByUserIdAsync(string userId)
		{
			bool result = await dbContext
				.Moderators
				.AnyAsync(m => m.UserId.ToString() == userId && m.IsApproved == true);

			return result;
		}

		public async Task<bool> ModeratorExistsByPhoneNumberAsync(string phoneNumber)
		{
			bool result = await dbContext
				.Moderators
				.AnyAsync(a => a.PhoneNumber == phoneNumber);

			return result;
		}

		public async Task<ModeratorOptOutViewModel> GetViewForOptingOut(string modId)
		{
			ModeratorOptOutViewModel moderator = await dbContext.Moderators
			   .Where(m => m.Id.ToString() == modId)
			   .Select(m => new ModeratorOptOutViewModel()
			   {
				   PhoneNumber = m.PhoneNumber
			   })
			   .FirstAsync();

			return moderator;
		}

		public async Task RemoveModeratorAsync(string modId)
		{
			Moderator mod = await dbContext
				.Moderators
				.Where(m => m.Id.ToString() == modId)
				.FirstAsync();

			dbContext.Moderators.Remove(mod);
			await dbContext.SaveChangesAsync();
		}
	}
}


