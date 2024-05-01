using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hawalayk_APP.Migrations
{
    public partial class address3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_cities_Cityid",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_governorates_Governorateid",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "Governorateid",
                table: "Addresses",
                newName: "GovernorateId");

            migrationBuilder.RenameColumn(
                name: "Cityid",
                table: "Addresses",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_Governorateid",
                table: "Addresses",
                newName: "IX_Addresses_GovernorateId");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_Cityid",
                table: "Addresses",
                newName: "IX_Addresses_CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_cities_CityId",
                table: "Addresses",
                column: "CityId",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_governorates_GovernorateId",
                table: "Addresses",
                column: "GovernorateId",
                principalTable: "governorates",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_cities_CityId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_governorates_GovernorateId",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "GovernorateId",
                table: "Addresses",
                newName: "Governorateid");

            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Addresses",
                newName: "Cityid");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_GovernorateId",
                table: "Addresses",
                newName: "IX_Addresses_Governorateid");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                newName: "IX_Addresses_Cityid");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_cities_Cityid",
                table: "Addresses",
                column: "Cityid",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_governorates_Governorateid",
                table: "Addresses",
                column: "Governorateid",
                principalTable: "governorates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
