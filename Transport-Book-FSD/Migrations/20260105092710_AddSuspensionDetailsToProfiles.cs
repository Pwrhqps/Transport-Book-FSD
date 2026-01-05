using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportBookFSD.Migrations
{
    /// <inheritdoc />
    public partial class AddSuspensionDetailsToProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "SuspendedAt",
                table: "PassengerProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SuspendedReason",
                table: "PassengerProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SuspendedUntil",
                table: "PassengerProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SuspendedAt",
                table: "DriverProfiles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SuspendedReason",
                table: "DriverProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "SuspendedUntil",
                table: "DriverProfiles",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuspendedAt",
                table: "PassengerProfiles");

            migrationBuilder.DropColumn(
                name: "SuspendedReason",
                table: "PassengerProfiles");

            migrationBuilder.DropColumn(
                name: "SuspendedUntil",
                table: "PassengerProfiles");

            migrationBuilder.DropColumn(
                name: "SuspendedAt",
                table: "DriverProfiles");

            migrationBuilder.DropColumn(
                name: "SuspendedReason",
                table: "DriverProfiles");

            migrationBuilder.DropColumn(
                name: "SuspendedUntil",
                table: "DriverProfiles");
        }
    }
}
