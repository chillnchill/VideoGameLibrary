using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGameLibrary.Data.Models;

namespace VideoGameLibrary.Data.Configurations
{
    public class PlatformEntityConfiguration : IEntityTypeConfiguration<Platform>
    {
        public void Configure(EntityTypeBuilder<Platform> builder)
        {
           builder.HasData(GeneratePlatforms());
        }

        private Platform[] GeneratePlatforms()
        {
            return new Platform[]
            {
                 new() { Id = 1, Name = "PC" },
                 new() { Id = 2, Name = "PlayStation 5" },
                 new() { Id = 3, Name = "Xbox Series X" },
                 new() { Id = 4, Name = "Nintendo Switch" },
                 new() { Id = 5, Name = "PlayStation 4" },
                 new() { Id = 6, Name = "Xbox One" },
            };
        }
    }
}
