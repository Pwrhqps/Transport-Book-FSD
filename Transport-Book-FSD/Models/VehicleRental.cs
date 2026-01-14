using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Transport_Book_FSD.Models;

namespace Transport_Book_FSD.Models
{
    public class VehicleRental
    {
        [Key]
        public int VehicleRentalId { get; set; }

        [Required]
        public int VehicleId { get; set; }

        // Store the Identity user id of the driver (string)
        [Required, StringLength(450)]
        public string DriverId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int Days { get; set; }

        public DateTime? EndDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal? TotalCost { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        // optional navigation
        public Vehicle Vehicle { get; set; }
    }
}