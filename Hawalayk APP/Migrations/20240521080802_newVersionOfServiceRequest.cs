using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hawalayk_APP.Migrations
{
    public partial class newVersionOfServiceRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Governorate",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "ServiceRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "Governorate",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "ServiceRequests");
        }
    }
}
