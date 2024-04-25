using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hawalayk_APP.Migrations
{
    public partial class address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_cities_CityId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_governorates_GovernorateId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReports_AspNetUsers_ReportedUserId",
                table: "UserReports");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReports_AspNetUsers_ReporterId",
                table: "UserReports");

            migrationBuilder.DropIndex(
                name: "IX_UserReports_ReporterId",
                table: "UserReports");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fa542aab-5326-4cdc-a1a0-b1a5ffd994f6");

            migrationBuilder.DropColumn(
                name: "ReporterId",
                table: "UserReports");

            migrationBuilder.RenameColumn(
                name: "ReportedUserId",
                table: "UserReports",
                newName: "ReporerId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReports_ReportedUserId",
                table: "UserReports",
                newName: "IX_UserReports_ReporerId");

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

            migrationBuilder.AddColumn<string>(
                name: "ReporedId",
                table: "UserReports",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "governorates",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
                //.Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "cities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
                //.Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_UserReports_ReporedId",
                table: "UserReports",
                column: "ReporedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_cities_Cityid",
                table: "Addresses",
                column: "Cityid",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_governorates_Governorateid",
                table: "Addresses",
                column: "Governorateid",
                principalTable: "governorates",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReports_AspNetUsers_ReporedId",
                table: "UserReports",
                column: "ReporedId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReports_AspNetUsers_ReporerId",
                table: "UserReports",
                column: "ReporerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_cities_Cityid",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_governorates_Governorateid",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReports_AspNetUsers_ReporedId",
                table: "UserReports");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReports_AspNetUsers_ReporerId",
                table: "UserReports");

            migrationBuilder.DropIndex(
                name: "IX_UserReports_ReporedId",
                table: "UserReports");

            migrationBuilder.DropColumn(
                name: "ReporedId",
                table: "UserReports");

            migrationBuilder.RenameColumn(
                name: "ReporerId",
                table: "UserReports",
                newName: "ReportedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserReports_ReporerId",
                table: "UserReports",
                newName: "IX_UserReports_ReportedUserId");

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

            migrationBuilder.AddColumn<string>(
                name: "ReporterId",
                table: "UserReports",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "governorates",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
            .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "cities",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fa542aab-5326-4cdc-a1a0-b1a5ffd994f6", 0, new DateTime(2024, 3, 8, 19, 42, 31, 959, DateTimeKind.Local).AddTicks(4272), "1355045c-a7c6-45a1-b71a-773230d666ef", "a@gmail.com", false, "User", "Male", "User", false, null, null, null, "AQAAAAEAACcQAAAAEFQWxExeZzjODI7TVFPJl/OZmT/smQMZQwz/MeUVJHiJCXN5mke3AgyMYkhx+IJEdA==", "010000000000", false, "j.jpg", "f76c0190-0a8b-4473-91f8-d9cc90f756fd", false, "User" });

            migrationBuilder.CreateIndex(
                name: "IX_UserReports_ReporterId",
                table: "UserReports",
                column: "ReporterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_cities_CityId",
                table: "Addresses",
                column: "CityId",
                principalTable: "cities",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_governorates_GovernorateId",
                table: "Addresses",
                column: "GovernorateId",
                principalTable: "governorates",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReports_AspNetUsers_ReportedUserId",
                table: "UserReports",
                column: "ReportedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReports_AspNetUsers_ReporterId",
                table: "UserReports",
                column: "ReporterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
