using System.ComponentModel.DataAnnotations;

namespace Transport_Book_FSD.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }

        public int BookingId { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [StringLength(300)]
        public string? Comment { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
