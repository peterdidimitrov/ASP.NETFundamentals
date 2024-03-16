using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeminarHub.Data.Migrations
{
    public partial class ChangeDeleteBehaviorOnRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarParticipants_Seminars_SeminarId",
                table: "SeminarParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarParticipants_Seminars_SeminarId",
                table: "SeminarParticipants",
                column: "SeminarId",
                principalTable: "Seminars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarParticipants_Seminars_SeminarId",
                table: "SeminarParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarParticipants_Seminars_SeminarId",
                table: "SeminarParticipants",
                column: "SeminarId",
                principalTable: "Seminars",
                principalColumn: "Id");
        }
    }
}
