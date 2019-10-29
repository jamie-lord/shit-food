using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShitFood.Api.Migrations
{
    public partial class AddedTripAdvisor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TripAdvisor",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false),
                    PlaceId = table.Column<Guid>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    Summary = table.Column<string>(nullable: true),
                    Details = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripAdvisor", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_TripAdvisor_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripAdvisor_PlaceId",
                table: "TripAdvisor",
                column: "PlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_TripAdvisor_Rating",
                table: "TripAdvisor",
                column: "Rating");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripAdvisor");
        }
    }
}
