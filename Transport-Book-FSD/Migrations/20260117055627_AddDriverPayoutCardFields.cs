using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportBookFSD.Migrations
{
    /// <inheritdoc />
    public partial class AddDriverPayoutCardFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PayoutCardLast4",
                table: "DriverProfiles",
                type: "nvarchar(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PayoutCardName",
                table: "DriverProfiles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayoutCardLast4",
                table: "DriverProfiles");

            migrationBuilder.DropColumn(
                name: "PayoutCardName",
                table: "DriverProfiles");
        }
    }
}
