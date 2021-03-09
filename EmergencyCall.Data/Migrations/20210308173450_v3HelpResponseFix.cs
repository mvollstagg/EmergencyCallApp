using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmergencyCall.Data.Migrations
{
    public partial class v3HelpResponseFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HelpResponses_HelpRequests_HelpRequestId",
                table: "HelpResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpResponses_Users_UserId",
                table: "HelpResponses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "HelpResponses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HelpResponses");

            migrationBuilder.DropColumn(
                name: "RecordedAtDate",
                table: "HelpResponses");

            migrationBuilder.DropColumn(
                name: "SlugUrl",
                table: "HelpResponses");

            migrationBuilder.DropColumn(
                name: "UpdatedAtDate",
                table: "HelpResponses");

            migrationBuilder.DropColumn(
                name: "UrlId",
                table: "HelpResponses");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "HelpResponses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HelpRequestId",
                table: "HelpResponses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HelpResponses_HelpRequests_HelpRequestId",
                table: "HelpResponses",
                column: "HelpRequestId",
                principalTable: "HelpRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HelpResponses_Users_UserId",
                table: "HelpResponses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HelpResponses_HelpRequests_HelpRequestId",
                table: "HelpResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_HelpResponses_Users_UserId",
                table: "HelpResponses");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "HelpResponses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HelpRequestId",
                table: "HelpResponses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "HelpResponses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HelpResponses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecordedAtDate",
                table: "HelpResponses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SlugUrl",
                table: "HelpResponses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAtDate",
                table: "HelpResponses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UrlId",
                table: "HelpResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_HelpResponses_HelpRequests_HelpRequestId",
                table: "HelpResponses",
                column: "HelpRequestId",
                principalTable: "HelpRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HelpResponses_Users_UserId",
                table: "HelpResponses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
