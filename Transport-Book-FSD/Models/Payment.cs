using System.ComponentModel.DataAnnotations;

namespace Transport_Book_FSD.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public decimal Amount { get; set; }

        [Required, StringLength(30)]
        public string Method { get; set; } = "Card";

        [Required, StringLength(20)]
        public string Status { get; set; } = PaymentStatuses.Pending;

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public static class PaymentStatuses
        {
            public const string Pending = "Pending";
            public const string Processing = "Processing";
            public const string Succeeded = "Succeeded";
            public const string Failed = "Failed";
            public const string Cancelled = "Cancelled";
        }

        [Required, StringLength(30)]
        public string ReferenceNo { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? CompletedAt { get; set; }

        // Optional: save only last 4 digits (realistic, not full card)
        [StringLength(4)]
        public string? CardLast4 { get; set; }
    }
}
