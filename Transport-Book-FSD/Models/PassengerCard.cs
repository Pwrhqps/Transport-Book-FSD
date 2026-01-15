using System.ComponentModel.DataAnnotations;

namespace TransportBookFSD.Models
{
    public class PassengerCard
    {
        [Key]
        public int PassengerCardId { get; set; }

        [Required]
        public string UserId { get; set; } = "";

        [Required, StringLength(80)]
        public string CardholderName { get; set; } = "";

        [Required, StringLength(4)]
        public string Last4 { get; set; } = "";

        [Range(1, 12)]
        public int ExpMonth { get; set; }

        [Range(2024, 2100)]
        public int ExpYear { get; set; }

        public bool IsDefault { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
