using System.ComponentModel.DataAnnotations;

namespace TransportBookFSD.Models
{
    public class WalletTransaction
    {
        [Key]
        public int WalletTransactionId { get; set; }

        [Required]
        public string UserId { get; set; } = "";

        // + amount for TopUp, - amount for RidePayment
        public decimal Amount { get; set; }

        [Required, StringLength(20)]
        public string Type { get; set; } = ""; // "TopUp" / "RidePayment"

        [Required, StringLength(20)]
        public string Method { get; set; } = ""; // "Card" / "Balance" / "Cash"

        [StringLength(40)]
        public string ReferenceNo { get; set; } = "";

        public int? BookingId { get; set; }
        public int? PassengerCardId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
