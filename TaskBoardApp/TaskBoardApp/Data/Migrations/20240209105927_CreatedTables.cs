using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskBoardApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreatedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0d057a1d-c987-4b97-b521-5e621a2bf23b", 0, "e5d3973a-93ac-473c-a0fc-68c4367d9e00", null, false, false, null, "TEST@SOFTUNI.BG", null, "AQAAAAIAAYagAAAAEMchVclrRLwlL942RSiZ2YGJnPIdCUcwu7gIT3PuF6IjGNC0VumLb50uNwgKJ979Jg==", null, false, "abd5c0e7-eb00-42ca-995e-c860f5d657f9", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "InProgress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 24, 12, 59, 27, 133, DateTimeKind.Local).AddTicks(6862), "Implement better styling for all public pages", "0d057a1d-c987-4b97-b521-5e621a2bf23b", "Improve CSS styles" },
                    { 2, 1, new DateTime(2023, 9, 9, 12, 59, 27, 133, DateTimeKind.Local).AddTicks(6915), "Create Android client app for the TaskBoard RESTful API", "0d057a1d-c987-4b97-b521-5e621a2bf23b", "Android Client App" },
                    { 3, 2, new DateTime(2024, 1, 9, 12, 59, 27, 133, DateTimeKind.Local).AddTicks(6919), "Create Windows Forms desktop app client for the TaskBoard RESTful API", "0d057a1d-c987-4b97-b521-5e621a2bf23b", "Desktop Client App" },
                    { 4, 3, new DateTime(2023, 2, 9, 12, 59, 27, 133, DateTimeKind.Local).AddTicks(6922), "Implement [Create Task] page for adding new tasks", "0d057a1d-c987-4b97-b521-5e621a2bf23b", "Create Tasks" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0d057a1d-c987-4b97-b521-5e621a2bf23b");

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Boards",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
