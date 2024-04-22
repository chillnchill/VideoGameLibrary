using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VideoGameLibrary.Data.Models;

namespace VideoGameLibrary.Data.Configurations
{
	public class ModeratorEntityConfiguration : IEntityTypeConfiguration<Moderator>
    {
        public void Configure(EntityTypeBuilder<Moderator> builder)
        {
            builder.HasData(GenerateModerator());
        }
        private Moderator[] GenerateModerator()
        {
            ICollection<Moderator> moderators = new HashSet<Moderator>();
            Moderator moderator;

            moderator = new Moderator()
            {              
                PhoneNumber = "+359767845979",
                AboutMe = "Enthusiastic gamer with a passion for fostering a positive and informative gaming community.",
                IsApproved = true,
                UserId = Guid.Parse("6B1E9650-EA91-4E1D-B0CB-E0B9061B4363"),
            };
            moderators.Add(moderator);

            moderator = new Moderator()
            {            
                PhoneNumber = "+359656717434",
                AboutMe = "The sheriff in these here parts, see?.",
                IsApproved = true,
				UserId = Guid.Parse("53DE18C9-A65B-412E-A006-12D968768F59"),
            };
            moderators.Add(moderator);

            return moderators.ToArray();
        }
    }
}
