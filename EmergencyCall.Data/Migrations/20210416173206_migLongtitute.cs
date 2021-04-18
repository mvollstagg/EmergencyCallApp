using Microsoft.EntityFrameworkCore.Migrations;

namespace EmergencyCall.Data.Migrations
{
    public partial class migLongtitute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Altitude",
                table: "HelpRequests",
                newName: "Longtitute");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longtitute",
                table: "HelpRequests",
                newName: "Altitude");
        }
    }
}
