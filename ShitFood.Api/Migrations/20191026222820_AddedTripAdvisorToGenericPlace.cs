using Microsoft.EntityFrameworkCore.Migrations;

namespace ShitFood.Api.Migrations
{
    public partial class AddedTripAdvisorToGenericPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TripAdvisor_PlaceId",
                table: "TripAdvisor");

            migrationBuilder.CreateIndex(
                name: "IX_TripAdvisor_PlaceId",
                table: "TripAdvisor",
                column: "PlaceId",
                unique: true,
                filter: "[PlaceId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TripAdvisor_PlaceId",
                table: "TripAdvisor");

            migrationBuilder.CreateIndex(
                name: "IX_TripAdvisor_PlaceId",
                table: "TripAdvisor",
                column: "PlaceId");
        }
    }
}
