using Microsoft.EntityFrameworkCore.Migrations;

namespace EmergencyCall.Data.Migrations
{
    public partial class usersAltitudeFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Altitude",
                table: "Users",
                newName: "Longtitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longtitude",
                table: "Users",
                newName: "Altitude");
        }
    }
}
