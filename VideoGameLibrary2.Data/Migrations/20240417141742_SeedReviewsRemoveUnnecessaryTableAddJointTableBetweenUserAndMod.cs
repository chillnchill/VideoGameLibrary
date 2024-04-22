using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameLibrary.Data.Migrations
{
    public partial class SeedReviewsRemoveUnnecessaryTableAddJointTableBetweenUserAndMod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamesPlatforms");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("61277b6b-d4b8-4223-8675-ac5b94e3b70d"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("d6dcc7b3-b818-42dc-a851-4f2fc7162257"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("f31e35ac-8eff-4135-86f7-2906eb98cbb6"));

            migrationBuilder.DeleteData(
                table: "Moderators",
                keyColumn: "Id",
                keyValue: new Guid("60f11725-2d1e-43ad-a6d5-6c262695925f"));

            migrationBuilder.DeleteData(
                table: "Moderators",
                keyColumn: "Id",
                keyValue: new Guid("f0e8bf22-45b4-4534-9279-7e82b38be5ca"));

            migrationBuilder.CreateTable(
                name: "UsersModerators",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModeratorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersModerators", x => new { x.UserId, x.ModeratorId });
                    table.ForeignKey(
                        name: "FK_UsersModerators_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersModerators_Moderators_ModeratorId",
                        column: x => x.ModeratorId,
                        principalTable: "Moderators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("53de18c9-a65b-412e-a006-12d968768f59"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5bb401e1-5e70-46ba-9de7-7aa137b1c0eb", "AQAAAAEAACcQAAAAEHRDlyEGrm/1YCpW+1OzZctHpC/u/Jr1cUW0GNtKk+wW2gJZTpvD/Bc/OQYpWqFUdQ==", "741423315B113C24A70F55A4CD6F0EAC" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de6b9eb9-7961-4a91-91cd-99712852fe24", "AQAAAAEAACcQAAAAENlfo9lE2bO8mSRXM2TSr26+WvtVKXw7VutungN+uaQNQ4cuIlvmjpatpy/+PJLj0Q==", "741423315B113C24A70F55A4CD6F0EAC" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a756f78d-b7cb-4934-a596-15a8ed7a12f1", "AQAAAAEAACcQAAAAELRoEjXtC67HMiY7+F8u9WBeLhZXAXqwoFfLU6b7TTwg+m6T4pjEfBRSlsR+sSq9Qw==", "741423315B113C24A70F55A4CD6F0EAC" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "ApplicationUserId", "CoverImg", "Description", "Developer", "GenreId", "IsDeleted", "NumberOfStars", "OwnerId", "PlatformId", "Price", "Publisher", "Rating", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("57448a25-0b32-416c-9049-0e566199cc82"), null, "/images/game-covers/gta-v.jpg", "An open world action-adventure game set in Los Angeles.", "Rockstar North", 2, false, 4.7000000000000002, new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363"), 3, 29.99m, "Rockstar Games", "M 17+", new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grand Theft Auto V" },
                    { new Guid("5895c747-b392-4551-80aa-d1a43a6b6b7f"), null, "/images/game-covers/minecraft.jpg", "A sandbox game where players explore a blocky world and build structures.", "Mojang Studios", 3, false, 4.5, new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"), 4, 19.99m, "Mojang Studios", "E 10+", new DateTime(2011, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Minecraft" },
                    { new Guid("abdd8aa5-2b86-4121-a13e-e5f119c553cb"), null, "/images/game-covers/breath-of-the-wild.jpg", "An open-air adventure game where players explore a vast and ruined Hyrule.", "Nintendo", 1, false, 4.7999999999999998, new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"), 1, 59.99m, "Nintendo", "E 10+", new DateTime(2017, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Legend of Zelda: Breath of the Wild" }
                });

            migrationBuilder.InsertData(
                table: "Moderators",
                columns: new[] { "Id", "AboutMe", "IsApproved", "PhoneNumber", "UserId" },
                values: new object[,]
                {
                    { new Guid("9065a1eb-7f8f-4c8f-a7dc-914c627fc495"), "The sheriff in these here parts, see?.", true, "+359656717434", new Guid("53de18c9-a65b-412e-a006-12d968768f59") },
                    { new Guid("a9a34cba-225e-49db-9093-1747452557a3"), "Enthusiastic gamer with a passion for fostering a positive and informative gaming community.", true, "+359767845979", new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersModerators_ModeratorId",
                table: "UsersModerators",
                column: "ModeratorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersModerators");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("57448a25-0b32-416c-9049-0e566199cc82"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("5895c747-b392-4551-80aa-d1a43a6b6b7f"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("abdd8aa5-2b86-4121-a13e-e5f119c553cb"));

            migrationBuilder.DeleteData(
                table: "Moderators",
                keyColumn: "Id",
                keyValue: new Guid("9065a1eb-7f8f-4c8f-a7dc-914c627fc495"));

            migrationBuilder.DeleteData(
                table: "Moderators",
                keyColumn: "Id",
                keyValue: new Guid("a9a34cba-225e-49db-9093-1747452557a3"));

            migrationBuilder.CreateTable(
                name: "GamesPlatforms",
                columns: table => new
                {
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlatformId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesPlatforms", x => new { x.GameId, x.PlatformId });
                    table.ForeignKey(
                        name: "FK_GamesPlatforms_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamesPlatforms_Platforms_PlatformId",
                        column: x => x.PlatformId,
                        principalTable: "Platforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("53de18c9-a65b-412e-a006-12d968768f59"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "682a3875-978b-4c41-bc41-90ace5380f6c", "AQAAAAEAACcQAAAAEKDc4Y3aropP6c9c+EkK6dejHbJBJGnFJK9SP+ZoZiQrWxVnEfbYwx6S4V9m0U7qHw==", "08E0AD5163911CB76E6945DDEE404984" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84b6c373-c0bc-4552-b672-936372ea7baa", "AQAAAAEAACcQAAAAECL2caPlrI0eKTrxoH1GSevHxIZWGGuUhSyUJgmKZpCaf9Wmcn07oz6i7W7hfP6JBA==", "08E0AD5163911CB76E6945DDEE404984" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6192c20e-f523-456e-a000-cc7408f5c6a2", "AQAAAAEAACcQAAAAEG73Xtl6dZXisbbgYYeSfAA1zVCKurcn92cm/pS/ZI++DaJ3a6Ane3VKOIcf08uG7w==", "08E0AD5163911CB76E6945DDEE404984" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "ApplicationUserId", "CoverImg", "Description", "Developer", "GenreId", "IsDeleted", "NumberOfStars", "OwnerId", "PlatformId", "Price", "Publisher", "Rating", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("61277b6b-d4b8-4223-8675-ac5b94e3b70d"), null, "/images/game-covers/gta-v.jpg", "An open world action-adventure game set in Los Angeles.", "Rockstar North", 2, false, 4.7000000000000002, new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363"), 3, 29.99m, "Rockstar Games", "M 17+", new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grand Theft Auto V" },
                    { new Guid("d6dcc7b3-b818-42dc-a851-4f2fc7162257"), null, "/images/game-covers/breath-of-the-wild.jpg", "An open-air adventure game where players explore a vast and ruined Hyrule.", "Nintendo", 1, false, 4.7999999999999998, new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"), 1, 59.99m, "Nintendo", "E 10+", new DateTime(2017, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Legend of Zelda: Breath of the Wild" },
                    { new Guid("f31e35ac-8eff-4135-86f7-2906eb98cbb6"), null, "/images/game-covers/minecraft.jpg", "A sandbox game where players explore a blocky world and build structures.", "Mojang Studios", 3, false, 4.5, new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"), 4, 19.99m, "Mojang Studios", "E 10+", new DateTime(2011, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Minecraft" }
                });

            migrationBuilder.InsertData(
                table: "Moderators",
                columns: new[] { "Id", "AboutMe", "IsApproved", "PhoneNumber", "UserId" },
                values: new object[,]
                {
                    { new Guid("60f11725-2d1e-43ad-a6d5-6c262695925f"), "The sheriff in these here parts, see?.", true, "+359656717434", new Guid("53de18c9-a65b-412e-a006-12d968768f59") },
                    { new Guid("f0e8bf22-45b4-4534-9279-7e82b38be5ca"), "Enthusiastic gamer with a passion for fostering a positive and informative gaming community.", true, "+359767845979", new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamesPlatforms_PlatformId",
                table: "GamesPlatforms",
                column: "PlatformId");
        }
    }
}
