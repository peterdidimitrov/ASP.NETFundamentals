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
                values: new object[] { "711bc276-fe99-4da6-950e-2765f9bff695", 0, "2b2d2c5f-7d10-4d99-8aca-13bb44debd79", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAIAAYagAAAAEI4ooWbwI4BIETkeydAAFwBZzTh/ihk5Ikv45eXLKneh9Mv4U0JImjwj/7OPhwZM0w==", null, false, "92472830-80d0-4350-816d-1f21d25a51e4", false, "test@softuni.bg" });

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
                    { 1, 1, new DateTime(2023, 7, 24, 15, 1, 50, 90, DateTimeKind.Local).AddTicks(9558), "Implement better styling for all public pages", "711bc276-fe99-4da6-950e-2765f9bff695", "Improve CSS styles" },
                    { 2, 1, new DateTime(2023, 9, 9, 15, 1, 50, 90, DateTimeKind.Local).AddTicks(9615), "Create Android client app for the TaskBoard RESTful API", "711bc276-fe99-4da6-950e-2765f9bff695", "Android Client App" },
                    { 3, 2, new DateTime(2024, 1, 9, 15, 1, 50, 90, DateTimeKind.Local).AddTicks(9620), "Create Windows Forms desktop app client for the TaskBoard RESTful API", "711bc276-fe99-4da6-950e-2765f9bff695", "Desktop Client App" },
                    { 4, 3, new DateTime(2023, 2, 9, 15, 1, 50, 90, DateTimeKind.Local).AddTicks(9623), "Implement [Create Task] page for adding new tasks", "711bc276-fe99-4da6-950e-2765f9bff695", "Create Tasks" }
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
                keyValue: "711bc276-fe99-4da6-950e-2765f9bff695");

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
