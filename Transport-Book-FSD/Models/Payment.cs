using System.ComponentModel.DataAnnotations;

namespace Transport_Book_FSD.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        // Who paid (bind to Identity user)
        [Required]
        public string PassengerUserId { get; set; } = string.Empty;

        // Transaction summary
        public decimal TotalAmount { get; set; }
        public int ItemCount { get; set; }

        [Required, StringLength(30)]
        public string Method { get; set; } = "Card";

        [Required, StringLength(20)]
        public string Status { get; set; } = PaymentStatuses.Pending;

        [Required, StringLength(30)]
        public string ReferenceNo { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? CompletedAt { get; set; }

        // Optional mock receipt detail
        [StringLength(4)]
        public string? CardLast4 { get; set; }

        // Navigation: one payment can cover many bookings
        public List<Booking> Bookings { get; set; } = new();

        public static class PaymentStatuses
        {
            public const string Pending = "Pending";
            public const string Processing = "Processing";
            public const string Succeeded = "Succeeded";
            public const string Failed = "Failed";
            public const string Cancelled = "Cancelled";
        }
    }
}
