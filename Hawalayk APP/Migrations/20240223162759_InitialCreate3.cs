using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hawalayk_APP.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CraftsMan_Crafts_CraftId",
                table: "CraftsMan");

            migrationBuilder.AlterColumn<int>(
                name: "CraftId",
                table: "CraftsMan",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CraftsMan_Crafts_CraftId",
                table: "CraftsMan",
                column: "CraftId",
                principalTable: "Crafts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CraftsMan_Crafts_CraftId",
                table: "CraftsMan");

            migrationBuilder.AlterColumn<int>(
                name: "CraftId",
                table: "CraftsMan",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CraftsMan_Crafts_CraftId",
                table: "CraftsMan",
                column: "CraftId",
                principalTable: "Crafts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
