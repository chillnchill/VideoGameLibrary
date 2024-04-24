namespace VideoGameLibrary.Tests
{
	using VideoGameLibrary.Data;
	using VideoGameLibrary.Data.Models;

	public static class DbSeeder
	{
		public static ApplicationUser NormalUser;
		public static ApplicationUser ModeratorUser;
		public static Moderator Moderator;
		public static Genre Genre;
		public static Platform Platform;
		public static Review Review;
		public static Game Game;

		public static void SeedGame(VideoGameLibraryDbContext dbContext)
		{
			Game = new Game()
			{
				Title = "The Legend of Zelda: Breath of the Wild",
				Description = "An open-air adventure game where players explore a vast and ruined Hyrule.",
				CoverImg = "/images/game-covers/breath-of-the-wild.jpg",
				Developer = "Nintendo",
				Publisher = "Nintendo",
				Rating = "E 10+",
				NumberOfStars = 4.8,
				ReleaseDate = new DateTime(2017, 3, 3),
				Price = 59.99m,
				GenreId = 1,
				PlatformId = 1,
				OwnerId = Guid.Parse("F45130AA-0EE9-4F38-8344-08DC48EC1E24")
			};

			dbContext.Games.Add(Game);
			dbContext.SaveChanges();
		}
		public static void SeedUsers(VideoGameLibraryDbContext dbContext)
		{
			NormalUser = new ApplicationUser()
			{
				UserName = "Peteto",
				NormalizedUserName = "PESHO",
				Email = "pesho@gaming.com",
				NormalizedEmail = "PESHO@GAMING.COM",
				EmailConfirmed = true,
				PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
				ConcurrencyStamp = "caf271d7-0ba7-4ab1-8d8d-6d0e3711c27d",
				SecurityStamp = "ca32c787-626e-4234-a4e4-8c94d85a2b1c",
				TwoFactorEnabled = false,
				FirstName = "Pesho",
				LastName = "Peshov",
				Nickname = "LittleDestroyer"
			};

			ModeratorUser = new ApplicationUser()
			{
				UserName = "Gogata",
				NormalizedUserName = "GOSHO",
				Email = "gosho@gaming.com",
				NormalizedEmail = "GOSHO@GAMING.COM",
				EmailConfirmed = true,
				PasswordHash = "8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92",
				ConcurrencyStamp = "8b51706e-f6e8-4dae-b240-54f856fb3004",
				SecurityStamp = "f6af46f5-74ba-43dc-927b-ad83497d0387",
				TwoFactorEnabled = false,
				FirstName = "Gosho",
				LastName = "Goshov",
				Nickname = "Destroyer"
			};

			Moderator = new Moderator()
			{
				PhoneNumber = "+359767845979",
				AboutMe = "Enthusiastic gamer with a passion for fostering a positive and informative gaming community.",
				IsApproved = true,
				User = ModeratorUser,
			};


			dbContext.Users.Add(NormalUser);
			dbContext.Users.Add(ModeratorUser);
			dbContext.Moderators.Add(Moderator);

			dbContext.SaveChanges();
		}

		public static void SeedReviews(VideoGameLibraryDbContext dbContext)
		{
			List<Review> reviews = new List<Review>()
			{
				new Review
				{
					OwnerId = Guid.NewGuid(),
					Content = "Great game, highly recommended!",
					Rating = 5,
					DatePosted = DateTime.UtcNow.AddDays(-5),
					GameId = Guid.NewGuid()
				},
				new Review
				{
					OwnerId = Guid.NewGuid(),
					Content = "Average game, could be better.",
					Rating = 3,
					DatePosted = DateTime.UtcNow.AddDays(-3),
					GameId = Guid.NewGuid()
				}
			};

			dbContext.Reviews.AddRange(reviews);
			dbContext.SaveChanges();
		}
		public static void SeedGenres(VideoGameLibraryDbContext dbContext)
		{
			var genre = new Genre()
			{
				Name = "Action-Adventure"
			};

			dbContext.Genres.Add(genre);
			dbContext.SaveChanges();
		}

		public static void SeedPlatforms(VideoGameLibraryDbContext dbContext)
		{
			var platform = new Platform()
			{
				Name = "PC"
			};

			dbContext.Platforms.Add(platform);
			dbContext.SaveChanges();
		}
	}
}
