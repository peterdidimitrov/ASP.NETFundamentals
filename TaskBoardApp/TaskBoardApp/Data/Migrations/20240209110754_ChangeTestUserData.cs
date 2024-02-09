using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTestUserData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0d057a1d-c987-4b97-b521-5e621a2bf23b");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "89b8049b-be07-46ae-a750-039595a9b21d", 0, "2e5b7c2d-bd9e-4837-bca6-6ce7ad1ffa26", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAIAAYagAAAAEPPFSRcyVEUwS2qE+XNUavf6J/ITIB5p0Ps1vAHqc3bA/8u7eOO/fsQxD0FTpAelDA==", null, false, "90acdf7b-8ab1-44f6-b0bf-31fe32912de1", false, "test@softuni.bg" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2023, 7, 24, 13, 7, 54, 3, DateTimeKind.Local).AddTicks(1389), "89b8049b-be07-46ae-a750-039595a9b21d" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2023, 9, 9, 13, 7, 54, 3, DateTimeKind.Local).AddTicks(1440), "89b8049b-be07-46ae-a750-039595a9b21d" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2024, 1, 9, 13, 7, 54, 3, DateTimeKind.Local).AddTicks(1445), "89b8049b-be07-46ae-a750-039595a9b21d" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2023, 2, 9, 13, 7, 54, 3, DateTimeKind.Local).AddTicks(1447), "89b8049b-be07-46ae-a750-039595a9b21d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "89b8049b-be07-46ae-a750-039595a9b21d");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0d057a1d-c987-4b97-b521-5e621a2bf23b", 0, "e5d3973a-93ac-473c-a0fc-68c4367d9e00", null, false, false, null, "TEST@SOFTUNI.BG", null, "AQAAAAIAAYagAAAAEMchVclrRLwlL942RSiZ2YGJnPIdCUcwu7gIT3PuF6IjGNC0VumLb50uNwgKJ979Jg==", null, false, "abd5c0e7-eb00-42ca-995e-c860f5d657f9", false, "test@softuni.bg" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2023, 7, 24, 12, 59, 27, 133, DateTimeKind.Local).AddTicks(6862), "0d057a1d-c987-4b97-b521-5e621a2bf23b" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2023, 9, 9, 12, 59, 27, 133, DateTimeKind.Local).AddTicks(6915), "0d057a1d-c987-4b97-b521-5e621a2bf23b" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2024, 1, 9, 12, 59, 27, 133, DateTimeKind.Local).AddTicks(6919), "0d057a1d-c987-4b97-b521-5e621a2bf23b" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2023, 2, 9, 12, 59, 27, 133, DateTimeKind.Local).AddTicks(6922), "0d057a1d-c987-4b97-b521-5e621a2bf23b" });
        }
    }
}
