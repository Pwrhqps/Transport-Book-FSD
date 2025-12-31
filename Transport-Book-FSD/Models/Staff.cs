using System.ComponentModel.DataAnnotations;

namespace Transport_Book_FSD.Models
{
    public class Staff
    {
        public int StaffId { get; set; }

        [Required, StringLength(120)]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(80)]
        public string Password { get; set; } = string.Empty;
    }
}
