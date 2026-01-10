using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportBookFSD.Migrations
{
    /// <inheritdoc />
    public partial class PhaseB_AddFeedbackUserIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE f
SET
    f.PassengerUserId = b.PassengerUserId,
    f.DriverUserId = b.DriverUserId
FROM Feedbacks f
JOIN Bookings b ON b.BookingId = f.BookingId
WHERE f.PassengerUserId IS NULL OR f.PassengerUserId = '';
");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
