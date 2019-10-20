using Microsoft.EntityFrameworkCore.Migrations;

namespace ShitFood.Api.Migrations
{
    public partial class AddedIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RatingValue",
                table: "FoodHygieneRating",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GooglePlaces_Rating",
                table: "GooglePlaces",
                column: "Rating");

            migrationBuilder.CreateIndex(
                name: "IX_FoodHygieneRating_RatingValue",
                table: "FoodHygieneRating",
                column: "RatingValue");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GooglePlaces_Rating",
                table: "GooglePlaces");

            migrationBuilder.DropIndex(
                name: "IX_FoodHygieneRating_RatingValue",
                table: "FoodHygieneRating");

            migrationBuilder.AlterColumn<string>(
                name: "RatingValue",
                table: "FoodHygieneRating",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
