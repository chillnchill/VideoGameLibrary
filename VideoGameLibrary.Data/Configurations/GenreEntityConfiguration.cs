using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGameLibrary.Data.Models;

namespace VideoGameLibrary.Data.Configurations
{
    public class GenreEntityConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(GenerateGenres());
        }

        private Genre[] GenerateGenres()
        {
            return new Genre[]
            {
                new() { Id = 1, Name = "Action-Adventure" },
                new() { Id = 2, Name = "Role-Playing Game (RPG)" },
                new() { Id = 3, Name = "Sandbox" },
                new() { Id = 4, Name = "Strategy" },
                new() { Id = 5, Name = "Simulation" },
                new() { Id = 6, Name = "Sports" },
            };
        }
    }
}
