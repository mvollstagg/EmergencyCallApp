using Microsoft.EntityFrameworkCore.Migrations;

namespace EmergencyCall.Data.Migrations
{
    public partial class migLongtitute2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Details",
                table: "HelpRequests",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "HelpRequests",
                newName: "Details");
        }
    }
}
