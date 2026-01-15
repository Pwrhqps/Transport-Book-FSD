using System.ComponentModel.DataAnnotations;

namespace TransportBookFSD.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }

        [Required, StringLength(120)]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(80)]
        public string Password { get; set; } = string.Empty;
    }
}
