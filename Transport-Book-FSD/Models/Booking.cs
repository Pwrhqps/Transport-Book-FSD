using System.ComponentModel.DataAnnotations;

namespace TransportBookFSD.Models
{
    public class Booking // Booking represents a ride request made by a passenger
    {
        public int BookingId { get; set; }

        // ===== Identity-based =====
        [Required, StringLength(450)]
        public string PassengerUserId { get; set; } = string.Empty;

        [StringLength(450)]
        public string? DriverUserId { get; set; }

        // ===== Booking details =====
        [Required, StringLength(120)]
        public string PickupLocation { get; set; } = string.Empty;

        [Required, StringLength(120)]
        public string DropoffLocation { get; set; } = string.Empty;

        public DateTime PickupDateTime { get; set; } = DateTime.Now;

        [StringLength(20)]
        public string Status { get; set; } = "Pending";

        public decimal Fare { get; set; }

        public DateTime? AcceptedAt { get; set; }

        public int? PaymentId { get; set; }
        public Payment? Payment { get; set; }

        public bool IsPaid { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}
  