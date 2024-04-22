using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using VideoGameLibrary.Data.Models;

namespace VideoGameLibrary.Data.Configurations
{
    public class GameEntityConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder
               .HasOne(p => p.Platform)
               .WithMany(p => p.Games)
               .HasForeignKey(p => p.PlatformId)
               .OnDelete(DeleteBehavior.Restrict);

            builder
               .HasOne(g => g.Genre)
               .WithMany(g => g.Games)
               .HasForeignKey(g => g.GenreId)
               .OnDelete(DeleteBehavior.Restrict);



			builder
               .Property(e => e.Price)
               .HasColumnType("decimal(18,2)");

            builder.HasData(GenerateGames());
        }

        private Game[] GenerateGames()
        {
            ICollection<Game> games = new HashSet<Game>();

            Game game;

            game = new Game()
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
            games.Add(game);

            game = new Game()
            {
                Title = "Grand Theft Auto V",
                Description =
      "An open world action-adventure game set in Los Angeles.",
                CoverImg = "/images/game-covers/gta-v.jpg", 
                Developer = "Rockstar North",
                Publisher = "Rockstar Games",
                Rating = "M 17+",
                NumberOfStars = 4.7,
                ReleaseDate = new DateTime(2013, 9, 17),
                Price = 29.99m,
                GenreId = 2,
                PlatformId = 3,
                OwnerId = Guid.Parse("6B1E9650-EA91-4E1D-B0CB-E0B9061B4363")

            };
            games.Add(game);

            game = new Game()
            {
                Title = "Minecraft",
                Description =
      "A sandbox game where players explore a blocky world and build structures.",
                CoverImg = "/images/game-covers/minecraft.jpg",
                Developer = "Mojang Studios",
                Publisher = "Mojang Studios",
                Rating = "E 10+",
                NumberOfStars = 4.5,
                ReleaseDate = new DateTime(2011, 11, 18),
                Price = 19.99m,
                GenreId = 3,
                PlatformId = 4,
                OwnerId = Guid.Parse("F45130AA-0EE9-4F38-8344-08DC48EC1E24")

            };

            games.Add(game);

            return games.ToArray();
        }
    }
}
