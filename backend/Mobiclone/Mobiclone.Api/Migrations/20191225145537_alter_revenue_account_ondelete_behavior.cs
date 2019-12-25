using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobiclone.Api.Migrations
{
    public partial class alter_revenue_account_ondelete_behavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Revenues_Accounts_AccountId",
                table: "Revenues");

            migrationBuilder.DropIndex(
                name: "IX_Revenues_AccountId",
                table: "Revenues");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_AccountId",
                table: "Revenues",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Revenues_Accounts_AccountId",
                table: "Revenues",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Revenues_Accounts_AccountId",
                table: "Revenues");

            migrationBuilder.DropIndex(
                name: "IX_Revenues_AccountId",
                table: "Revenues");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_AccountId",
                table: "Revenues",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Revenues_Accounts_AccountId",
                table: "Revenues",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
