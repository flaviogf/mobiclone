using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobiclone.Api.Migrations
{
    public partial class alter_expense_account_ondelete_behavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Accounts_AccountId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Revenues_AccountId",
                table: "Revenues");

            migrationBuilder.DropIndex(
                name: "IX_Outputs_FromId",
                table: "Outputs");

            migrationBuilder.DropIndex(
                name: "IX_Outputs_ToId",
                table: "Outputs");

            migrationBuilder.DropIndex(
                name: "IX_Inputs_FromId",
                table: "Inputs");

            migrationBuilder.DropIndex(
                name: "IX_Inputs_ToId",
                table: "Inputs");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_AccountId",
                table: "Revenues",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Outputs_FromId",
                table: "Outputs",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Outputs_ToId",
                table: "Outputs",
                column: "ToId");

            migrationBuilder.CreateIndex(
                name: "IX_Inputs_FromId",
                table: "Inputs",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Inputs_ToId",
                table: "Inputs",
                column: "ToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Accounts_AccountId",
                table: "Expenses",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Accounts_AccountId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Revenues_AccountId",
                table: "Revenues");

            migrationBuilder.DropIndex(
                name: "IX_Outputs_FromId",
                table: "Outputs");

            migrationBuilder.DropIndex(
                name: "IX_Outputs_ToId",
                table: "Outputs");

            migrationBuilder.DropIndex(
                name: "IX_Inputs_FromId",
                table: "Inputs");

            migrationBuilder.DropIndex(
                name: "IX_Inputs_ToId",
                table: "Inputs");

            migrationBuilder.CreateIndex(
                name: "IX_Revenues_AccountId",
                table: "Revenues",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Outputs_FromId",
                table: "Outputs",
                column: "FromId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Outputs_ToId",
                table: "Outputs",
                column: "ToId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inputs_FromId",
                table: "Inputs",
                column: "FromId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inputs_ToId",
                table: "Inputs",
                column: "ToId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Accounts_AccountId",
                table: "Expenses",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
