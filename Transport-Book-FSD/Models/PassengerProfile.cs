using System.ComponentModel.DataAnnotations;

namespace Transport_Book_FSD.Models
{
    public class PassengerProfile
    {
        [Key]
        public int PassengerProfileId { get; set; }

        [Required]
        public string UserId { get; set; } = "";

        [Required]
        public string FullName { get; set; } = "";

        [Required]
        public string ContactNumber { get; set; } = "";

        public string Gender { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }

        public string ResidentialAddress { get; set; } = "";
        public string EmergencyContact { get; set; } = "";

        public int TotalCompletedTrips { get; set; } = 0;
        public decimal CancellationPenaltyBalance { get; set; }

        // Optional later
        public string ProfileImagePath { get; set; } = "";
    }
}
