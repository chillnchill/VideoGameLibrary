using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VideoGameLibrary.Data.Models;

namespace VideoGameLibrary.Data
{
    public class VideoGameLibraryDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public VideoGameLibraryDbContext(DbContextOptions<VideoGameLibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Platform> Platforms { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Moderator> Moderators { get; set; } = null!;
        public DbSet<GameModerator> GamesModerators { get; set; } = null!;
        public DbSet<UserModerator> UsersModerators { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly configAssembly = Assembly.GetAssembly(typeof(VideoGameLibraryDbContext)) ??
                             Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(configAssembly);

            base.OnModelCreating(builder);
        }
    }
}
