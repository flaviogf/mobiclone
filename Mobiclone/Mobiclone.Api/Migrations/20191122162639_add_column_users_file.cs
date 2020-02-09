using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobiclone.Api.Migrations
{
    public partial class add_column_users_file : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_FileId",
                table: "Users",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Files_FileId",
                table: "Users",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Files_FileId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_FileId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "Users");
        }
    }
}
