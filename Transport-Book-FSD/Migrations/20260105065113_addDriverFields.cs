using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportBookFSD.Migrations
{
    /// <inheritdoc />
    public partial class addDriverFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LicensePlate",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VehicleType",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicensePlate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VehicleType",
                table: "AspNetUsers");
        }
    }
}
