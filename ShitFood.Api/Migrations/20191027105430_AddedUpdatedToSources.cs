using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShitFood.Api.Migrations
{
    public partial class AddedUpdatedToSources : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "TripAdvisor",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "GooglePlaces",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "FoodHygieneRating",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Updated",
                table: "TripAdvisor");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "GooglePlaces");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "FoodHygieneRating");
        }
    }
}
