using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpionAPI_DataAccess.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerCount = table.Column<int>(type: "int", nullable: false),
                    GuessDataID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UseRelatedWordAsAHint = table.Column<bool>(type: "bit", nullable: false),
                    SpyPresent = table.Column<bool>(type: "bit", nullable: false),
                    UndercoverPresent = table.Column<bool>(type: "bit", nullable: false),
                    PantonimePresent = table.Column<bool>(type: "bit", nullable: false),
                    SingerPresent = table.Column<bool>(type: "bit", nullable: false),
                    SpyWon = table.Column<bool>(type: "bit", nullable: false),
                    UndercoverWon = table.Column<bool>(type: "bit", nullable: false),
                    GameStarted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GameCompleted = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStatistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuessData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuessedWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelatedWord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuessData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GuessData_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuessData_CreatedByUserId",
                table: "GuessData",
                column: "CreatedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameStatistics");

            migrationBuilder.DropTable(
                name: "GuessData");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
