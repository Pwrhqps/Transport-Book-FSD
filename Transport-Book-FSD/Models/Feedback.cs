using System;
using System.ComponentModel.DataAnnotations;

namespace TransportBookFSD.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        public int BookingId { get; set; }

        public int PassengerId { get; set; }
        public int? DriverId { get; set; }
        [Required]
        public string PassengerUserId { get; set; } = string.Empty;
        public string? DriverUserId { get; set; } 

        public int Rating { get; set; }
        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
