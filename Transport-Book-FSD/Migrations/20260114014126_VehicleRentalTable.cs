using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportBookFSD.Migrations
{
    /// <inheritdoc />
    public partial class VehicleRentalTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "VehicleRentals",
                columns: table => new
                {
                    VehicleRentalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Days = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalCost = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleRentals", x => x.VehicleRentalId);
                    table.ForeignKey(
                        name: "FK_VehicleRentals_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleRentals_VehicleId",
                table: "VehicleRentals",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleRentals");

            migrationBuilder.AddColumn<int>(
                name: "Days",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Bookings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                table: "Bookings",
                type: "decimal(10,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_VehicleId",
                table: "Bookings",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Vehicles_VehicleId",
                table: "Bookings",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "VehicleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
