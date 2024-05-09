using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hawalayk_APP.Migrations
{
    public partial class newVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "craftName",
                table: "ServiceRequests",
                newName: "CraftId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_CraftId",
                table: "ServiceRequests",
                column: "CraftId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_Crafts_CraftId",
                table: "ServiceRequests",
                column: "CraftId",
                principalTable: "Crafts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_Crafts_CraftId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_CraftId",
                table: "ServiceRequests");

            migrationBuilder.RenameColumn(
                name: "CraftId",
                table: "ServiceRequests",
                newName: "craftName");
        }
    }
}
