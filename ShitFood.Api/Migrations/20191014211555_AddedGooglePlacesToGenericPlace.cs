using Microsoft.EntityFrameworkCore.Migrations;

namespace ShitFood.Api.Migrations
{
    public partial class AddedGooglePlacesToGenericPlace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GooglePlaces_PlaceId",
                table: "GooglePlaces");

            migrationBuilder.CreateIndex(
                name: "IX_GooglePlaces_PlaceId",
                table: "GooglePlaces",
                column: "PlaceId",
                unique: true,
                filter: "[PlaceId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GooglePlaces_PlaceId",
                table: "GooglePlaces");

            migrationBuilder.CreateIndex(
                name: "IX_GooglePlaces_PlaceId",
                table: "GooglePlaces",
                column: "PlaceId");
        }
    }
}
