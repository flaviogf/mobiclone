using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mobiclone.Api.Migrations
{
    public partial class create_table_outputs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Outputs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 255, nullable: false),
                    Value = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ToId = table.Column<int>(nullable: false),
                    FromId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Outputs_Accounts_FromId",
                        column: x => x.FromId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Outputs_Accounts_ToId",
                        column: x => x.ToId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Outputs");
        }
    }
}
