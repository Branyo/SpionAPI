using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpionAPI_DataAccess.Migrations
{
    public partial class RenameLastUsageToLastGameUsage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUsage",
                table: "GuessData",
                newName: "LastGameUsageTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastGameUsage",
                table: "GuessData",
                newName: "LastUsage");
        }
    }
}
