using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShitFood.Api.Migrations
{
    public partial class MakeFoodHygieneRatingOptionalRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodHygieneRating_Place_PlaceId",
                table: "FoodHygieneRating");

            migrationBuilder.DropIndex(
                name: "IX_FoodHygieneRating_PlaceId",
                table: "FoodHygieneRating");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlaceId",
                table: "FoodHygieneRating",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_FoodHygieneRating_PlaceId",
                table: "FoodHygieneRating",
                column: "PlaceId",
                unique: true,
                filter: "[PlaceId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodHygieneRating_Place_PlaceId",
                table: "FoodHygieneRating",
                column: "PlaceId",
                principalTable: "Place",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodHygieneRating_Place_PlaceId",
                table: "FoodHygieneRating");

            migrationBuilder.DropIndex(
                name: "IX_FoodHygieneRating_PlaceId",
                table: "FoodHygieneRating");

            migrationBuilder.AlterColumn<Guid>(
                name: "PlaceId",
                table: "FoodHygieneRating",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

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
    }
}
