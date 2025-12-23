using System.ComponentModel.DataAnnotations;

namespace Transport_Book_FSD.Models
{
    public class Passenger
    {
        public int PassengerId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;  // Identity FK

        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(20)]
        public string Phone { get; set; } = string.Empty;
    }
}
