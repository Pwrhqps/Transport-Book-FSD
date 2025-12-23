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
        public string Status { get; set; } = "Pending";

        public DateTime PaymentDate { get; set; } = DateTime.Now;
    }
}
