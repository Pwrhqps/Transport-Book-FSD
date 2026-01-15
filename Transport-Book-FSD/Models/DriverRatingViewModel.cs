
using System.ComponentModel.DataAnnotations;

namespace TransportBookFSD.Models
{
    public class DriverRatingViewModel
    {
        [Key]
        public int DriverId { get; set; }

        public string UserId { get; set; } = "";
        public string FullName { get; set; } = string.Empty;
        public double Rating { get; set; }
        public bool IsSuspended { get; set; }

        public string? SuspendedReason { get; set; }
        public DateTime? SuspendedUntil { get; set; }
    }
}