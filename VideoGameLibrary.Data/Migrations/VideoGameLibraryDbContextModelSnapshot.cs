﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VideoGameLibrary.Data;

#nullable disable

namespace VideoGameLibrary.Data.Migrations
{
    [DbContext(typeof(VideoGameLibraryDbContext))]
    partial class VideoGameLibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AboutMe")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("IsSubmitted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(22)
                        .HasColumnType("nvarchar(22)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a756f78d-b7cb-4934-a596-15a8ed7a12f1",
                            Email = "gamer@gaming.com",
                            EmailConfirmed = false,
                            FirstName = "User",
                            IsSubmitted = false,
                            LastName = "Userov",
                            LockoutEnabled = false,
                            Nickname = "usercho",
                            NormalizedEmail = "gamer@gaming.com",
                            NormalizedUserName = "gamer@gaming.com",
                            PasswordHash = "AQAAAAEAACcQAAAAELRoEjXtC67HMiY7+F8u9WBeLhZXAXqwoFfLU6b7TTwg+m6T4pjEfBRSlsR+sSq9Qw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "741423315B113C24A70F55A4CD6F0EAC",
                            TwoFactorEnabled = false,
                            UserName = "gamer@gaming.com"
                        },
                        new
                        {
                            Id = new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "de6b9eb9-7961-4a91-91cd-99712852fe24",
                            Email = "moderator@gaming.com",
                            EmailConfirmed = false,
                            FirstName = "Mod",
                            IsSubmitted = false,
                            LastName = "Modov",
                            LockoutEnabled = false,
                            Nickname = "modcho",
                            NormalizedEmail = "moderator@gaming.com",
                            NormalizedUserName = "moderator@gaming.com",
                            PasswordHash = "AQAAAAEAACcQAAAAENlfo9lE2bO8mSRXM2TSr26+WvtVKXw7VutungN+uaQNQ4cuIlvmjpatpy/+PJLj0Q==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "741423315B113C24A70F55A4CD6F0EAC",
                            TwoFactorEnabled = false,
                            UserName = "moderator@gaming.com"
                        },
                        new
                        {
                            Id = new Guid("53de18c9-a65b-412e-a006-12d968768f59"),
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "5bb401e1-5e70-46ba-9de7-7aa137b1c0eb",
                            Email = "admin@gaming.com",
                            EmailConfirmed = false,
                            FirstName = "Admin",
                            IsSubmitted = false,
                            LastName = "Adminov",
                            LockoutEnabled = false,
                            Nickname = "admincho",
                            NormalizedEmail = "admin@gaming.com",
                            NormalizedUserName = "admin@gaming.com",
                            PasswordHash = "AQAAAAEAACcQAAAAEHRDlyEGrm/1YCpW+1OzZctHpC/u/Jr1cUW0GNtKk+wW2gJZTpvD/Bc/OQYpWqFUdQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "741423315B113C24A70F55A4CD6F0EAC",
                            TwoFactorEnabled = false,
                            UserName = "admin@gaming.com"
                        });
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CoverImg")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("Developer")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<double?>("NumberOfStars")
                        .HasMaxLength(5)
                        .HasColumnType("float");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PlatformId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Publisher")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Rating")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("GenreId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("PlatformId");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            Id = new Guid("abdd8aa5-2b86-4121-a13e-e5f119c553cb"),
                            CoverImg = "/images/game-covers/breath-of-the-wild.jpg",
                            Description = "An open-air adventure game where players explore a vast and ruined Hyrule.",
                            Developer = "Nintendo",
                            GenreId = 1,
                            IsDeleted = false,
                            NumberOfStars = 4.7999999999999998,
                            OwnerId = new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"),
                            PlatformId = 1,
                            Price = 59.99m,
                            Publisher = "Nintendo",
                            Rating = "E 10+",
                            ReleaseDate = new DateTime(2017, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "The Legend of Zelda: Breath of the Wild"
                        },
                        new
                        {
                            Id = new Guid("57448a25-0b32-416c-9049-0e566199cc82"),
                            CoverImg = "/images/game-covers/gta-v.jpg",
                            Description = "An open world action-adventure game set in Los Angeles.",
                            Developer = "Rockstar North",
                            GenreId = 2,
                            IsDeleted = false,
                            NumberOfStars = 4.7000000000000002,
                            OwnerId = new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363"),
                            PlatformId = 3,
                            Price = 29.99m,
                            Publisher = "Rockstar Games",
                            Rating = "M 17+",
                            ReleaseDate = new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Grand Theft Auto V"
                        },
                        new
                        {
                            Id = new Guid("5895c747-b392-4551-80aa-d1a43a6b6b7f"),
                            CoverImg = "/images/game-covers/minecraft.jpg",
                            Description = "A sandbox game where players explore a blocky world and build structures.",
                            Developer = "Mojang Studios",
                            GenreId = 3,
                            IsDeleted = false,
                            NumberOfStars = 4.5,
                            OwnerId = new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"),
                            PlatformId = 4,
                            Price = 19.99m,
                            Publisher = "Mojang Studios",
                            Rating = "E 10+",
                            ReleaseDate = new DateTime(2011, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Minecraft"
                        });
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.GameModerator", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ModeratorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GameId", "ModeratorId");

                    b.HasIndex("ModeratorId");

                    b.ToTable("GamesModerators");
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.HasKey("Id");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Action-Adventure"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Role-Playing Game (RPG)"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Sandbox"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Strategy"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Simulation"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Sports"
                        });
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.Moderator", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AboutMe")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Moderators");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a9a34cba-225e-49db-9093-1747452557a3"),
                            AboutMe = "Enthusiastic gamer with a passion for fostering a positive and informative gaming community.",
                            IsApproved = true,
                            PhoneNumber = "+359767845979",
                            UserId = new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363")
                        },
                        new
                        {
                            Id = new Guid("9065a1eb-7f8f-4c8f-a7dc-914c627fc495"),
                            AboutMe = "The sheriff in these here parts, see?.",
                            IsApproved = true,
                            PhoneNumber = "+359656717434",
                            UserId = new Guid("53de18c9-a65b-412e-a006-12d968768f59")
                        });
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.Platform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("nvarchar(35)");

                    b.HasKey("Id");

                    b.ToTable("Platforms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "PC"
                        },
                        new
                        {
                            Id = 2,
                            Name = "PlayStation 5"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Xbox Series X"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Nintendo Switch"
                        },
                        new
                        {
                            Id = 5,
                            Name = "PlayStation 4"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Xbox One"
                        });
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<DateTime>("DatePosted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.Screenshot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Screenshots");
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.UserModerator", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ModeratorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "ModeratorId");

                    b.HasIndex("ModeratorId");

                    b.ToTable("UsersModerators");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("VideoGameLibrary.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("VideoGameLibrary.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VideoGameLibrary.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("VideoGameLibrary.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.Game", b =>
                {
                    b.HasOne("VideoGameLibrary.Data.Models.ApplicationUser", null)
                        .WithMany("LikedGames")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("VideoGameLibrary.Data.Models.Genre", "Genre")
                        .WithMany("Games")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VideoGameLibrary.Data.Models.ApplicationUser", "Owner")
                        .WithMany("AddedGames")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VideoGameLibrary.Data.Models.Platform", "Platform")
                        .WithMany("Games")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Owner");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.GameModerator", b =>
                {
                    b.HasOne("VideoGameLibrary.Data.Models.Game", "Game")
                        .WithMany("Moderators")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VideoGameLibrary.Data.Models.Moderator", "Moderator")
                        .WithMany("ManagedGames")
                        .HasForeignKey("ModeratorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("Moderator");
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.Moderator", b =>
                {
                    b.HasOne("VideoGameLibrary.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.Review", b =>
                {
                    b.HasOne("VideoGameLibrary.Data.Models.Game", "Game")
                        .WithMany("Reviews")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.Screenshot", b =>
                {
                    b.HasOne("VideoGameLibrary.Data.Models.Game", "Game")
                        .WithMany("Screenshots")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.UserModerator", b =>
                {
                    b.HasOne("VideoGameLibrary.Data.Models.Moderator", "Moderator")
                        .WithMany()
                        .HasForeignKey("ModeratorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VideoGameLibrary.Data.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Moderator");

                    b.Navigation("User");
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.ApplicationUser", b =>
                {
                    b.Navigation("AddedGames");

                    b.Navigation("LikedGames");
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.Game", b =>
                {
                    b.Navigation("Moderators");

                    b.Navigation("Reviews");

                    b.Navigation("Screenshots");
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.Genre", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.Moderator", b =>
                {
                    b.Navigation("ManagedGames");
                });

            modelBuilder.Entity("VideoGameLibrary.Data.Models.Platform", b =>
                {
                    b.Navigation("Games");
                });
#pragma warning restore 612, 618
        }
    }
}
