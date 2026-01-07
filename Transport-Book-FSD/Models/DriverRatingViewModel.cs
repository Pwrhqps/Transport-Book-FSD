
namespace Transport_Book_FSD.Models
{
    public class DriverRatingViewModel
    {
        public int DriverId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public double Rating { get; set; }
        public bool IsSuspended { get; set; }

        public string? SuspendedReason { get; set; }
        public DateTime? SuspendedUntil { get; set; }
    }
}