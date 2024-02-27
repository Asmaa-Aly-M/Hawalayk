using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hawalayk_APP.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_CraftsMan_CraftsmanId",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "CraftsmanId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_CraftsMan_CraftsmanId",
                table: "Posts",
                column: "CraftsmanId",
                principalTable: "CraftsMan",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_CraftsMan_CraftsmanId",
                table: "Posts");

            migrationBuilder.AlterColumn<string>(
                name: "CraftsmanId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_CraftsMan_CraftsmanId",
                table: "Posts",
                column: "CraftsmanId",
                principalTable: "CraftsMan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
