using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace ShitFood.Api.Migrations
{
    public partial class AddedGetShitRequestTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GetShitRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Requested = table.Column<DateTime>(nullable: false),
                    Location = table.Column<Point>(nullable: true),
                    Distance = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetShitRequest", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetShitRequest");
        }
    }
}
