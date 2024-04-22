using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Security.Cryptography;
using VideoGameLibrary.Data.Models;

namespace VideoGameLibrary.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasMany(u => u.AddedGames)
                .WithOne(g => g.Owner)
                .HasForeignKey(g => g.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(GenerateUser());
        }

        private ApplicationUser[] GenerateUser()
        {
            PasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
            byte[] randomBytes = RandomNumberGenerator.GetBytes(16);
            string securityStamp = BitConverter.ToString(randomBytes).Replace("-", "");

            ICollection<ApplicationUser> users = new HashSet<ApplicationUser>();
            ApplicationUser applicationUser;

            applicationUser = new ApplicationUser()
            {
                Id = Guid.Parse("F45130AA-0EE9-4F38-8344-08DC48EC1E24"),
                Email = "gamer@gaming.com",
                NormalizedEmail = "gamer@gaming.com",
                UserName = "gamer@gaming.com",
                NormalizedUserName = "gamer@gaming.com",
                FirstName = "User",
                LastName = "Userov",
                Nickname = "usercho",
            };

            applicationUser.PasswordHash = hasher.HashPassword(applicationUser, "123456");
            applicationUser.SecurityStamp = securityStamp;
            users.Add(applicationUser);

            applicationUser = new ApplicationUser()
            {
                Id = Guid.Parse("6B1E9650-EA91-4E1D-B0CB-E0B9061B4363"),
                Email = "moderator@gaming.com",
                NormalizedEmail = "moderator@gaming.com",
                UserName = "moderator@gaming.com",
                NormalizedUserName = "moderator@gaming.com",
                FirstName = "Mod",
                LastName = "Modov",
                Nickname = "modcho",
            };
            
            applicationUser.PasswordHash = hasher.HashPassword(applicationUser, "123456");
            applicationUser.SecurityStamp = securityStamp;
            users.Add(applicationUser);

            applicationUser = new ApplicationUser()
            {
                Id = Guid.Parse("53DE18C9-A65B-412E-A006-12D968768F59"),
                Email = "admin@gaming.com",
                NormalizedEmail = "admin@gaming.com",
                UserName = "admin@gaming.com",
                NormalizedUserName = "admin@gaming.com",
                FirstName = "Admin",
                LastName = "Adminov",
                Nickname = "admincho",
            };

            applicationUser.PasswordHash = hasher.HashPassword(applicationUser, "123456");
            applicationUser.SecurityStamp = securityStamp;
            users.Add(applicationUser);

            return users.ToArray();
        }
    };
}
