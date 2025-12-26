using System.ComponentModel.DataAnnotations;

namespace Transport_Book_FSD.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int PassengerId { get; set; }

        [Required, StringLength(120)]
        public string PickupLocation { get; set; } = string.Empty;

        [Required, StringLength(120)]
        public string DropoffLocation { get; set; } = string.Empty;

        public DateTime PickupDateTime { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string Status { get; set; } = "Pending";

        public decimal Fare { get; set; }

        public int? DriverId { get; set; }     // assigned driver
       
        public DateTime? AcceptedAt { get; set; }

        public bool IsPaid { get; set; }
        
        public DateTime? PaidAt { get; set; }

    }
}
