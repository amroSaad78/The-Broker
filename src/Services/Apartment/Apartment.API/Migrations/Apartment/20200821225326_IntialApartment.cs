using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Apartment.API.Migrations.Apartment
{
    public partial class IntialApartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "apartment_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "bedroom_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "country_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "furniture_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "period_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "rent_hilo",
                incrementBy: 10);

            migrationBuilder.CreateSequence(
                name: "sale_hilo",
                incrementBy: 10);

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
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    OwnerId = table.Column<int>(nullable: false, defaultValueSql: "0"),
                    BedroomId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    FurnitureId = table.Column<int>(nullable: false),
                    PictureFileName = table.Column<string>(nullable: true),
                    PictureUri = table.Column<string>(nullable: true),
                    BookedUp = table.Column<bool>(nullable: false),
                    RequestId = table.Column<Guid>(nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Rent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ApartmentId = table.Column<int>(nullable: false),
                    PeriodId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rent_Apartment_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rent_Period_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Period",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ApartmentId = table.Column<int>(nullable: false),
                    Installment = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sale_Apartment_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartment",
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
                name: "IX_Rent_ApartmentId",
                table: "Rent",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_PeriodId",
                table: "Rent",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_Sale_ApartmentId",
                table: "Sale",
                column: "ApartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rent");

            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.DropTable(
                name: "Period");

            migrationBuilder.DropTable(
                name: "Apartment");

            migrationBuilder.DropTable(
                name: "Bedroom");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "Furniture");

            migrationBuilder.DropSequence(
                name: "apartment_hilo");

            migrationBuilder.DropSequence(
                name: "bedroom_hilo");

            migrationBuilder.DropSequence(
                name: "country_hilo");

            migrationBuilder.DropSequence(
                name: "furniture_hilo");

            migrationBuilder.DropSequence(
                name: "period_hilo");

            migrationBuilder.DropSequence(
                name: "rent_hilo");

            migrationBuilder.DropSequence(
                name: "sale_hilo");
        }
    }
}
