using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShitFood.Api.Migrations
{
    public partial class AddedFoodHygieneRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PlaceId",
                table: "FoodHygieneRating",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_FoodHygieneRating_PlaceId",
                table: "FoodHygieneRating",
                column: "PlaceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodHygieneRating_Place_PlaceId",
                table: "FoodHygieneRating",
                column: "PlaceId",
                principalTable: "Place",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodHygieneRating_Place_PlaceId",
                table: "FoodHygieneRating");

            migrationBuilder.DropIndex(
                name: "IX_FoodHygieneRating_PlaceId",
                table: "FoodHygieneRating");

            migrationBuilder.DropColumn(
                name: "PlaceId",
                table: "FoodHygieneRating");
        }
    }
}
