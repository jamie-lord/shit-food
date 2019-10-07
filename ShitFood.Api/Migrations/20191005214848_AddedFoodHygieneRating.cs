using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShitFood.Api.Migrations
{
    public partial class AddedFoodHygieneRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodHygieneRating",
                columns: table => new
                {
                    FHRSID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                    Longitude = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    RightToReply = table.Column<string>(nullable: true),
                    Distance = table.Column<double>(nullable: false),
                    NewRatingPending = table.Column<bool>(nullable: false),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodHygieneRating", x => x.FHRSID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodHygieneRating");
        }
    }
}
