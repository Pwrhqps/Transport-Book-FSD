using System;
using System.ComponentModel.DataAnnotations;

namespace Transport_Book_FSD.Models
{
    public class DriverProfile
    {
        [Key]
        public int DriverProfileId { get; set; }

        [Required]
        public string UserId { get; set; } = "";

        [Required, StringLength(100)]
        public string FullName { get; set; } = "";

        [Required, StringLength(20)]
        public string ContactNumber { get; set; } = "";

        [Required, StringLength(10)]
        public string Gender { get; set; } = "Male";

        public DateTime? DateOfBirth { get; set; }

        // Vehicle / license (basic for now)
        [StringLength(50)]
        public string VehicleType { get; set; } = "";

        [StringLength(50)]
        public string VehicleModel { get; set; } = "";

        [StringLength(20)]
        public string VehiclePlate { get; set; } = "";

        [StringLength(30)]
        public string LicenseNumber { get; set; } = "";

        // Read-only later (updated by system)
        public int TotalCompletedTrips { get; set; }
        public decimal TotalEarnings { get; set; }
        public double Rating { get; set; } = 5.0;
    }
}
