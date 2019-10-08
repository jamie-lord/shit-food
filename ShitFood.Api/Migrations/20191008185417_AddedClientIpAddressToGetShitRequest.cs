using Microsoft.EntityFrameworkCore.Migrations;

namespace ShitFood.Api.Migrations
{
    public partial class AddedClientIpAddressToGetShitRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientIpAddress",
                table: "GetShitRequest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientIpAddress",
                table: "GetShitRequest");
        }
    }
}
