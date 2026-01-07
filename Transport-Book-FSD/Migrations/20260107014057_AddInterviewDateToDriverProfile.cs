using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportBookFSD.Migrations
{
    /// <inheritdoc />
    public partial class AddInterviewDateToDriverProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "InterviewDate",
                table: "DriverProfiles",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InterviewDate",
                table: "DriverProfiles");
        }
    }
}
