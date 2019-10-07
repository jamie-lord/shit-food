using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace ShitFood.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<Point>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodHygieneRating",
                columns: table => new
                {
                    FHRSID = table.Column<int>(nullable: false),
                    PlaceId = table.Column<Guid>(nullable: true),
                    LocalAuthorityBusinessID = table.Column<string>(nullable: true),
                    BusinessName = table.Column<string>(nullable: true),
                    BusinessType = table.Column<string>(nullable: true),
                    BusinessTypeID = table.Column<int>(nullable: false),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    AddressLine3 = table.Column<string>(nullable: true),
                    AddressLine4 = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    RatingValue = table.Column<string>(nullable: true),
                    RatingKey = table.Column<string>(nullable: true),
                    RatingDate = table.Column<DateTime>(nullable: false),
                    LocalAuthorityCode = table.Column<string>(nullable: true),
                    LocalAuthorityName = table.Column<string>(nullable: true),
                    LocalAuthorityWebSite = table.Column<string>(nullable: true),
                    LocalAuthorityEmailAddress = table.Column<string>(nullable: true),
                    Hygiene = table.Column<int>(nullable: true),
                    Structural = table.Column<int>(nullable: true),
                    ConfidenceInManagement = table.Column<int>(nullable: true),
                    SchemeType = table.Column<string>(nullable: true),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    RightToReply = table.Column<string>(nullable: true),
                    Distance = table.Column<double>(nullable: false),
                    NewRatingPending = table.Column<bool>(nullable: false),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodHygieneRating", x => x.FHRSID);
                    table.ForeignKey(
                        name: "FK_FoodHygieneRating_Place_PlaceId",
                        column: x => x.PlaceId,
                        principalTable: "Place",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodHygieneRating_PlaceId",
                table: "FoodHygieneRating",
                column: "PlaceId",
                unique: true,
                filter: "[PlaceId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodHygieneRating");

            migrationBuilder.DropTable(
                name: "Place");
        }
    }
}
