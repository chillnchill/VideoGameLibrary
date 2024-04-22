using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameLibrary.Data.Migrations
{
    public partial class RemoveUnecessaryAssignmentToIsSubmitted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("53de18c9-a65b-412e-a006-12d968768f59"),
                columns: new[] { "ConcurrencyStamp", "IsSubmitted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "682a3875-978b-4c41-bc41-90ace5380f6c", false, "AQAAAAEAACcQAAAAEKDc4Y3aropP6c9c+EkK6dejHbJBJGnFJK9SP+ZoZiQrWxVnEfbYwx6S4V9m0U7qHw==", "08E0AD5163911CB76E6945DDEE404984" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6b1e9650-ea91-4e1d-b0cb-e0b9061b4363"),
                columns: new[] { "ConcurrencyStamp", "IsSubmitted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "84b6c373-c0bc-4552-b672-936372ea7baa", false, "AQAAAAEAACcQAAAAECL2caPlrI0eKTrxoH1GSevHxIZWGGuUhSyUJgmKZpCaf9Wmcn07oz6i7W7hfP6JBA==", "08E0AD5163911CB76E6945DDEE404984" });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
