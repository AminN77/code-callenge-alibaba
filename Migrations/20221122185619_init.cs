using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Code_Challenge.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weathers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Latitude = table.Column<float>(type: "real", nullable: false),
                    Longitude = table.Column<float>(type: "real", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true),
                    LastUpdateDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weathers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Weathers",
                columns: new[] { "Id", "Data", "LastUpdateDateTime", "Latitude", "Longitude" },
                values: new object[] { new Guid("81eb1a8a-166e-46b4-b329-36d00d5588f2"), "sunny", new DateTime(2022, 11, 22, 18, 56, 19, 179, DateTimeKind.Utc).AddTicks(3430), 0f, 0f });

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_Latitude",
                table: "Weathers",
                column: "Latitude");

            migrationBuilder.CreateIndex(
                name: "IX_Weathers_Longitude",
                table: "Weathers",
                column: "Longitude");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weathers");
        }
    }
}
