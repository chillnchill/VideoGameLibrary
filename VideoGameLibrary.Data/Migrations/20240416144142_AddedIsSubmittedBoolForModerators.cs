using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameLibrary.Data.Migrations
{
    public partial class AddedIsSubmittedBoolForModerators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("57dd648b-973c-4216-94e9-658cf1828f23"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("7c0bf7a2-d2d4-44ff-a6c8-d6f4e01861e1"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("d1f80a7b-511f-4384-b0c2-9b325225f7e5"));

            migrationBuilder.DeleteData(
                table: "Moderators",
                keyColumn: "Id",
                keyValue: new Guid("8c9c4d4b-2ab8-4d71-9892-655ce0b8baab"));

            migrationBuilder.DeleteData(
                table: "Moderators",
                keyColumn: "Id",
                keyValue: new Guid("f2e1e19d-195d-4ad9-95c0-394d62252f5b"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("53de18c9-a65b-412e-a006-12d968768f59"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74b5a8c1-0f87-40ab-8921-ba3de15172f3", "AQAAAAEAACcQAAAAECXAUmsWSnr6GWUuFEc5WTTb5BIBuMfzDYMLSQm1+dUUAUldQU7lABHjORNgcsB6hQ==", "895C636FF1127B7B180A98819DB6030E" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ead701c6-d0ff-432b-aa28-84ab56adb9bd", "AQAAAAEAACcQAAAAEAXdhlxv8fTwitAzo0+Xbx0WzqbvTOveM5bEfExGz1RyXpU5n4rAufPhGi4vHDYvCw==", "895C636FF1127B7B180A98819DB6030E" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1c99c0fe-8877-4016-a262-7467cdfd3d3e", "AQAAAAEAACcQAAAAEAwW41chWeTGpnH9+IxKf92Bpn6nh69oPXjfTDAincmEmzRdDEd5ZI56u3oWwCAang==", "895C636FF1127B7B180A98819DB6030E" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "ApplicationUserId", "CoverImg", "Description", "Developer", "GenreId", "IsDeleted", "NumberOfStars", "OwnerId", "PlatformId", "Price", "Publisher", "Rating", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("57dd648b-973c-4216-94e9-658cf1828f23"), null, "/images/game-covers/gta-v.jpg", "An open world action-adventure game set in Los Angeles.", "Rockstar North", 2, false, 4.7000000000000002, new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363"), 3, 29.99m, "Rockstar Games", "M 17+", new DateTime(2013, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Grand Theft Auto V" },
                    { new Guid("7c0bf7a2-d2d4-44ff-a6c8-d6f4e01861e1"), null, "/images/game-covers/breath-of-the-wild.jpg", "An open-air adventure game where players explore a vast and ruined Hyrule.", "Nintendo", 1, false, 4.7999999999999998, new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"), 1, 59.99m, "Nintendo", "E 10+", new DateTime(2017, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Legend of Zelda: Breath of the Wild" },
                    { new Guid("d1f80a7b-511f-4384-b0c2-9b325225f7e5"), null, "/images/game-covers/minecraft.jpg", "A sandbox game where players explore a blocky world and build structures.", "Mojang Studios", 3, false, 4.5, new Guid("f45130aa-0ee9-4f38-8344-08dc48ec1e24"), 4, 19.99m, "Mojang Studios", "E 10+", new DateTime(2011, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Minecraft" }
                });

            migrationBuilder.InsertData(
                table: "Moderators",
                columns: new[] { "Id", "AboutMe", "IsApproved", "PhoneNumber", "UserId" },
                values: new object[,]
                {
                    { new Guid("8c9c4d4b-2ab8-4d71-9892-655ce0b8baab"), "The sheriff in these here parts, see?.", true, "+359656717434", new Guid("53de18c9-a65b-412e-a006-12d968768f59") },
                    { new Guid("f2e1e19d-195d-4ad9-95c0-394d62252f5b"), "Enthusiastic gamer with a passion for fostering a positive and informative gaming community.", true, "+359767845979", new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363") }
                });
        }
    }
}
