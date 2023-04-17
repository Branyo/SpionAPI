using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpionAPI_DataAccess.Migrations
{
    public partial class AddLastUsageAttributeToGuessData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUsage",
                table: "GuessData",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUsage",
                table: "GuessData");
        }
    }
}
