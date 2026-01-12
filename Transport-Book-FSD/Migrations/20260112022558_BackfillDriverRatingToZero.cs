using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransportBookFSD.Migrations
{
    public partial class BackfillDriverRatingToZero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Reset default rating for drivers with no trips yet
            migrationBuilder.Sql(@"
UPDATE DriverProfiles
SET Rating = 0
WHERE Rating = 5
  AND TotalCompletedTrips = 0
  AND TotalEarnings = 0;
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No rollback needed (rating recalculates from feedback anyway)
        }
    }
}
