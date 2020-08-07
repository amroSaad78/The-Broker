using Microsoft.EntityFrameworkCore.Migrations;

namespace Apartment.API.Migrations
{
    public partial class IntialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "Apartment_hilo",
                incrementBy: 1);

            migrationBuilder.CreateSequence(
                name: "bedroom_hilo",
                incrementBy: 1);

            migrationBuilder.CreateSequence(
                name: "country_hilo",
                incrementBy: 1);

            migrationBuilder.CreateSequence(
                name: "furniture_hilo",
                incrementBy: 1);

            migrationBuilder.CreateSequence(
                name: "period_hilo",
                incrementBy: 1);

            migrationBuilder.CreateTable(
                name: "Bedroom",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BedroomsCount = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bedroom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Country = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Furniture",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    FurnitureType = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Furniture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Period",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Period = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Period", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apartment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Parking = table.Column<bool>(nullable: false),
                    Reception = table.Column<int>(nullable: false),
                    Kitchens = table.Column<int>(nullable: false),
                    Bathrooms = table.Column<int>(nullable: false),
                    Area = table.Column<int>(nullable: false),
                    View = table.Column<string>(maxLength: 50, nullable: true),
                    Floor = table.Column<int>(nullable: false),
                    Flat = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: false),
                    Region = table.Column<string>(maxLength: 100, nullable: false),
                    Adresse = table.Column<string>(maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Installment = table.Column<bool>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false, defaultValueSql: "0"),
                    BedroomId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    FurnitureId = table.Column<int>(nullable: false),
                    PeriodId = table.Column<int>(nullable: false),
                    Purpose = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apartment_Bedroom_BedroomId",
                        column: x => x.BedroomId,
                        principalTable: "Bedroom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apartment_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apartment_Furniture_FurnitureId",
                        column: x => x.FurnitureId,
                        principalTable: "Furniture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Apartment_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_BedroomId",
                table: "Apartment",
                column: "BedroomId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_CountryId",
                table: "Apartment",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_FurnitureId",
                table: "Apartment",
                column: "FurnitureId");

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_PeriodId",
                table: "Apartment",
                column: "PeriodId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apartment");

            migrationBuilder.DropTable(
                name: "Bedroom");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Furniture");

            migrationBuilder.DropTable(
                name: "Period");

            migrationBuilder.DropSequence(
                name: "Apartment_hilo");

            migrationBuilder.DropSequence(
                name: "bedroom_hilo");

            migrationBuilder.DropSequence(
                name: "country_hilo");

            migrationBuilder.DropSequence(
                name: "furniture_hilo");

            migrationBuilder.DropSequence(
                name: "period_hilo");
        }
    }
}
