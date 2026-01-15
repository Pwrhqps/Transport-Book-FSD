using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransportBookFSD.Models;

namespace TransportBookFSD.Data
{
    public class TransportBookFSDContext : DbContext
    {
        public TransportBookFSDContext (DbContextOptions<TransportBookFSDContext> options)
            : base(options)
        {
        }

        public DbSet<TransportBookFSD.Models.DriverProfile> DriverProfile { get; set; } = default!;
        public DbSet<TransportBookFSD.Models.Feedback> Feedback { get; set; } = default!;
        public DbSet<TransportBookFSD.Models.PassengerProfile> PassengerProfile { get; set; } = default!;
        public DbSet<TransportBookFSD.Models.Booking> Booking { get; set; } = default!;
        public DbSet<TransportBookFSD.Models.DriverRatingViewModel> DriverRatingViewModel { get; set; } = default!;
        public DbSet<TransportBookFSD.Models.Staff> Staff { get; set; } = default!;
        public DbSet<TransportBookFSD.Models.Vehicle> Vehicle { get; set; } = default!;
        public DbSet<TransportBookFSD.Models.VehicleRental> VehicleRental { get; set; } = default!;
    }
}
