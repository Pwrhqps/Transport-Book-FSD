using System.ComponentModel.DataAnnotations;

namespace TransportBookFSD.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }

        [Required, StringLength(100)]
        public string Make { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string Model { get; set; } = string.Empty;

        public int? Year { get; set; }

        [StringLength(50)]
        public string? LicensePlate { get; set; }

        [StringLength(50)]
        public string? VehicleType { get; set; }

        public bool IsAvailable { get; set; } = true;

        [DataType(DataType.Currency)]
        public decimal? DailyRate { get; set; }

        [StringLength(50)]
        public string? Colour { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }
    }
}