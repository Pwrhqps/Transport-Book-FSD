using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportBookFSD.Migrations
{
    /// <inheritdoc />
    public partial class BackfillBookingUserIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PassengerUserId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriverUserId",
                table: "Bookings",
                type: "nvarchar(450)",
                nullable: true);

            // Backfill PassengerUserId from legacy Passengers table
            migrationBuilder.Sql(@"
UPDATE b
SET b.PassengerUserId = p.UserId
FROM Bookings b
INNER JOIN Passengers p ON p.PassengerId = b.PassengerId
WHERE (b.PassengerUserId IS NULL OR b.PassengerUserId = '')
  AND p.UserId IS NOT NULL;
");

            // 3) Backfill DriverUserId from DriverProfiles (Booking.DriverId stores DriverProfileId)
            migrationBuilder.Sql(@"
UPDATE b
SET b.DriverUserId = d.UserId
FROM Bookings b
INNER JOIN DriverProfiles d ON d.DriverProfileId = b.DriverId
WHERE b.DriverId IS NOT NULL
  AND (b.DriverUserId IS NULL OR b.DriverUserId = '')
  AND d.UserId IS NOT NULL;
");

            // 4) Make sure PassengerUserId is never NULL (keep app safe)
            migrationBuilder.Sql(@"
UPDATE Bookings
SET PassengerUserId = ''
WHERE PassengerUserId IS NULL;
");

            // 5) Create indexes if they don't already exist
            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Bookings_PassengerUserId' AND object_id = OBJECT_ID('Bookings'))
    CREATE INDEX IX_Bookings_PassengerUserId ON Bookings(PassengerUserId);
");

            migrationBuilder.Sql(@"
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Bookings_DriverUserId' AND object_id = OBJECT_ID('Bookings'))
    CREATE INDEX IX_Bookings_DriverUserId ON Bookings(DriverUserId);
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop indexes first (safe)
            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Bookings_PassengerUserId' AND object_id = OBJECT_ID('Bookings'))
    DROP INDEX IX_Bookings_PassengerUserId ON Bookings;
");

            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Bookings_DriverUserId' AND object_id = OBJECT_ID('Bookings'))
    DROP INDEX IX_Bookings_DriverUserId ON Bookings;
");

            migrationBuilder.DropColumn(
                name: "DriverUserId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "PassengerUserId",
                table: "Bookings");
        }
    }
}
