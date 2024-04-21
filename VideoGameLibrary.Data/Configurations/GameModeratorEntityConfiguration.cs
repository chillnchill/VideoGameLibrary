using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGameLibrary.Data.Models;

namespace VideoGameLibrary.Data.Configurations
{
    internal class GameModeratorEntityConfiguration : IEntityTypeConfiguration<GameModerator>
    {
        public void Configure(EntityTypeBuilder<GameModerator> builder)
        {
            builder
              .HasKey(gp => new { gp.GameId, gp.ModeratorId });

            builder
                .HasOne(gm => gm.Moderator)
                .WithMany(m => m.ManagedGames)
                .HasForeignKey(gm => gm.ModeratorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
