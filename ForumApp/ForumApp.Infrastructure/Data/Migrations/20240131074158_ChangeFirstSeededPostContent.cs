using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForumApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFirstSeededPostContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Content",
                value: "First First Post Content");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Content",
                value: "First Post Content");
        }
    }
}
