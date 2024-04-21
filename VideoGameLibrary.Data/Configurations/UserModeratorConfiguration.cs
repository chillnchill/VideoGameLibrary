using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGameLibrary.Data.Models;

namespace VideoGameLibrary.Data.Configurations
{
    public class UserModeratorConfiguration : IEntityTypeConfiguration<UserModerator>
    {
        public void Configure(EntityTypeBuilder<UserModerator> builder)
        {

            builder
                .HasKey(um => new { um.UserId, um.ModeratorId });

            builder
                .HasOne(um => um.User)
                .WithMany()
                .HasForeignKey(um => um.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(um => um.Moderator)
                .WithMany()
                .HasForeignKey(um => um.ModeratorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
