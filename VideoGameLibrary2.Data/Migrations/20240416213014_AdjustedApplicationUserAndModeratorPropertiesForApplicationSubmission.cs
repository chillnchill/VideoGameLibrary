using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameLibrary.Data.Migrations
{
    public partial class AdjustedApplicationUserAndModeratorPropertiesForApplicationSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("7d013e67-bd8f-4ab8-ab60-9abfb694d5f1"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b34c7584-2cda-4cb5-b153-1d4886e4f4f1"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("f0e5837e-24d5-4c17-91e7-4fafe56edff9"));

            migrationBuilder.DeleteData(
                table: "Moderators",
                keyColumn: "Id",
                keyValue: new Guid("7237f229-d513-4806-b8b1-e17a2a71286e"));

            migrationBuilder.DeleteData(
                table: "Moderators",
                keyColumn: "Id",
                keyValue: new Guid("ff7ce209-d47e-4829-a472-acf81d4150f8"));

            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "Moderators");

            migrationBuilder.AddColumn<string>(
                name: "AboutMe",
                table: "AspNetUsers",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("53de18c9-a65b-412e-a006-12d968768f59"),
                columns: new[] { "ConcurrencyStamp", "IsSubmitted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dc3166ab-bfc2-4ed4-b8e2-d82c1915f28c", true, "AQAAAAEAACcQAAAAENUU8FGikdZ+BWn1Qnw47ZSjPLDts3T14Pr2SmFlxkVwKDGG8QYDZ36vrnAIbJ4GyA==", "8783258F98A68C94953D2D85E5ACC57A" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363"),
                columns: new[] { "ConcurrencyStamp", "IsSubmitted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8231af7-84af-4fab-b063-2698ecd38b35", true, "AQAAAAEAACcQAAAAEGDeYiGvPDOQiGCyJbg/8Z6/BSGbVUdNkPfxQaX2C89rNODRdrHAbeONWBKu3NwswQ==", "8783258F98A68C94953D2D85E5ACC57A" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54a6cc23-eb71-4236-8042-2330cb253931", "AQAAAAEAACcQAAAAEOIJjHQur6d7yZKHHCjWUx9ZdqQ/Dt8PlDepta4Q38T+g3TKSncb3JnLCTeBgd9wug==", "8783258F98A68C94953D2D85E5ACC57A" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "ApplicationUserId", "CoverImg", "Description", "Developer", "GenreId", "IsDeleted", "NumberOfStars", "OwnerId", "PlatformId", "Price", "Publisher", "Rating", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("160e968f-2f9a-4660-a826-1b78b0048ee4"), null, "/images/game-covers/gta-v.jpg", "An open world action-adventure game set in Los Angeles.", "Rockstar North", 2, false, 4.7000000000000002, new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363"), 3, 29.99m, "Rockstar Games", "M 17+", new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grand Theft Auto V" },
                    { new Guid("945289cf-4e7f-4266-8479-259a1d67385a"), null, "/images/game-covers/minecraft.jpg", "A sandbox game where players explore a blocky world and build structures.", "Mojang Studios", 3, false, 4.5, new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"), 4, 19.99m, "Mojang Studios", "E 10+", new DateTime(2011, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Minecraft" },
                    { new Guid("c0390882-b9bf-47b5-86fb-2a273f8ac0be"), null, "/images/game-covers/breath-of-the-wild.jpg", "An open-air adventure game where players explore a vast and ruined Hyrule.", "Nintendo", 1, false, 4.7999999999999998, new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"), 1, 59.99m, "Nintendo", "E 10+", new DateTime(2017, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Legend of Zelda: Breath of the Wild" }
                });

            migrationBuilder.InsertData(
                table: "Moderators",
                columns: new[] { "Id", "AboutMe", "IsApproved", "PhoneNumber", "UserId" },
                values: new object[,]
                {
                    { new Guid("53252d56-5e2e-44f1-9b73-876c104c747f"), "Enthusiastic gamer with a passion for fostering a positive and informative gaming community.", true, "+359767845979", new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363") },
                    { new Guid("9f9b0913-ac78-44a3-be1d-2cc7afd945a1"), "The sheriff in these here parts, see?.", true, "+359656717434", new Guid("53de18c9-a65b-412e-a006-12d968768f59") }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("160e968f-2f9a-4660-a826-1b78b0048ee4"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("945289cf-4e7f-4266-8479-259a1d67385a"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("c0390882-b9bf-47b5-86fb-2a273f8ac0be"));

            migrationBuilder.DeleteData(
                table: "Moderators",
                keyColumn: "Id",
                keyValue: new Guid("53252d56-5e2e-44f1-9b73-876c104c747f"));

            migrationBuilder.DeleteData(
                table: "Moderators",
                keyColumn: "Id",
                keyValue: new Guid("9f9b0913-ac78-44a3-be1d-2cc7afd945a1"));

            migrationBuilder.DropColumn(
                name: "AboutMe",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsSubmitted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubmitted",
                table: "Moderators",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("53de18c9-a65b-412e-a006-12d968768f59"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8212f668-d8ee-473e-9efe-d2526be9c483", "AQAAAAEAACcQAAAAECs2oxzKmX10bNZ9FU/PB7vpjvq12Ed8+AYaMjKoppwVOjV0rAiieplYcecx7D7vxQ==", "B73C200E3A36B5E89DBBD99BC52514C0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "725866ba-2786-4118-8df9-f357c9a47330", "AQAAAAEAACcQAAAAEFwgwudU14UZ1BPSKwXSx+s/rUu0H+gZtEqfFML+zt5etDRMFi3VjL6qldv1AffkOA==", "B73C200E3A36B5E89DBBD99BC52514C0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f403392-5b79-4b4c-8745-661c30510f8c", "AQAAAAEAACcQAAAAENWgN8YcaJVUPAJpQrCFiuf5l4bnUiIZCdR313Bta1Gz6JnFCkNk+XUmXBXfiDhO+Q==", "B73C200E3A36B5E89DBBD99BC52514C0" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "ApplicationUserId", "CoverImg", "Description", "Developer", "GenreId", "IsDeleted", "NumberOfStars", "OwnerId", "PlatformId", "Price", "Publisher", "Rating", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("7d013e67-bd8f-4ab8-ab60-9abfb694d5f1"), null, "/images/game-covers/minecraft.jpg", "A sandbox game where players explore a blocky world and build structures.", "Mojang Studios", 3, false, 4.5, new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"), 4, 19.99m, "Mojang Studios", "E 10+", new DateTime(2011, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Minecraft" },
                    { new Guid("b34c7584-2cda-4cb5-b153-1d4886e4f4f1"), null, "/images/game-covers/gta-v.jpg", "An open world action-adventure game set in Los Angeles.", "Rockstar North", 2, false, 4.7000000000000002, new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363"), 3, 29.99m, "Rockstar Games", "M 17+", new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grand Theft Auto V" },
                    { new Guid("f0e5837e-24d5-4c17-91e7-4fafe56edff9"), null, "/images/game-covers/breath-of-the-wild.jpg", "An open-air adventure game where players explore a vast and ruined Hyrule.", "Nintendo", 1, false, 4.7999999999999998, new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"), 1, 59.99m, "Nintendo", "E 10+", new DateTime(2017, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Legend of Zelda: Breath of the Wild" }
                });

            migrationBuilder.InsertData(
                table: "Moderators",
                columns: new[] { "Id", "AboutMe", "IsApproved", "IsSubmitted", "PhoneNumber", "UserId" },
                values: new object[,]
                {
                    { new Guid("7237f229-d513-4806-b8b1-e17a2a71286e"), "The sheriff in these here parts, see?.", true, true, "+359656717434", new Guid("53de18c9-a65b-412e-a006-12d968768f59") },
                    { new Guid("ff7ce209-d47e-4829-a472-acf81d4150f8"), "Enthusiastic gamer with a passion for fostering a positive and informative gaming community.", true, true, "+359767845979", new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363") }
                });
        }
    }
}
