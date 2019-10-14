using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShitFood.Api.Migrations
{
    public partial class UpdateGooglePlaces : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GooglePlaces",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PlaceId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Rating = table.Column<double>(nullable: false),
                    UserRatingsTotal = table.Column<int>(nullable: false),
                    PermanentlyClosed = table.Column<bool>(nullable: false),
                    PriceLevel = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GooglePlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GooglePlaces_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GooglePlaces_PlaceId",
                table: "GooglePlaces",
                column: "PlaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GooglePlaces");
        }
    }
}
