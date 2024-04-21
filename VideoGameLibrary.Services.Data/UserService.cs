using Microsoft.EntityFrameworkCore;
using VideoGameLibrary.Data;
using VideoGameLibrary.Data.Models;
using VideoGameLibrary.Services.Data.Interfaces;
using VideoGameLibrary.Web.ViewModels.User;


namespace VideoGameLibrary.Services.Data
{
	public class UserService : IUserService
	{
		private readonly VideoGameLibraryDbContext dbContext;

		public UserService(VideoGameLibraryDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddLikedGameAsync(string gameId, string userId)
		{
			Game game = await dbContext
				.Games
				.AsNoTracking()
				.Where(g => g.Id.ToString() == gameId && g.IsDeleted == false)
				.FirstAsync();

			ApplicationUser user = await dbContext
				.Users
				.Include(l => l.LikedGames)
				.Where(u => u.Id.ToString() == userId)
				.FirstAsync();

			user.LikedGames.Add(game);
			await dbContext.SaveChangesAsync();

		}

		public async Task UnlikeGameAsync(string userId, string gameId)
		{
			Game? game = await dbContext
				.Games
				.Where(g => g.Id.ToString().ToLower() == gameId.ToLower() && g.IsDeleted == false)
				.FirstOrDefaultAsync();

			ApplicationUser user = await dbContext
				.Users
				.Include(l => l.LikedGames)
				.Where(u => u.Id.ToString() == userId)
				.FirstAsync();

			user.LikedGames.Remove(game!);
			await dbContext.SaveChangesAsync();

		}

		public async Task<bool> IsGameInLikedListAsync(string gameId, string userId)
		{
			ApplicationUser user = await dbContext
				.Users
				.Include(l => l.LikedGames)
				.Where(u => u.Id.ToString() == userId)
				.FirstAsync();

			if (user.LikedGames.Any(g => g.Id.ToString() == gameId.ToLower()))
			{
				return true;
			}

			return false;
		}

		public async Task<string> GetUserNameById(string userId)
		{
			var userName = await dbContext
				.Users
				.FirstOrDefaultAsync(u => u.Id.ToString() == userId);

			if (userName == null)
			{
				return string.Empty;
			}

			return userName.UserName;
		}

		public async Task<string> GetNicknameByEmailAsync(string email)
		{
			ApplicationUser? user = await this.dbContext
			   .Users
			   .FirstOrDefaultAsync(u => u.Email == email);
			if (user == null)
			{
				return string.Empty;
			}

			return $"{user.Nickname}";
		}

		public async Task<IEnumerable<UserViewModel>> AllAsync()
		{
			List<UserViewModel> allUsers = await this.dbContext
				.Users
				.Select(u => new UserViewModel()
				{
					Id = u.Id.ToString(),
					Email = u.Email,
					Nickname = u.Nickname,
					FullName = u.FirstName + " " + u.LastName
				})
				.ToListAsync();

			foreach (UserViewModel user in allUsers)
			{
				Moderator? moderator = this.dbContext
					.Moderators
					.FirstOrDefault(a => a.UserId.ToString() == user.Id);
				if (moderator != null)
				{
					user.PhoneNumber = moderator.PhoneNumber;
				}
				else
				{
					user.PhoneNumber = string.Empty;
				}
			}

			return allUsers;
		}

		public async Task<bool> GetIsSubmittedByEmailAsync(string email)
		{
			ApplicationUser? user = await this.dbContext
			  .Users
			  .FirstOrDefaultAsync(u => u.Email == email);

			if (user != null)
			{
				return user.IsSubmitted;
			}

			return false;
		}
	}
}
