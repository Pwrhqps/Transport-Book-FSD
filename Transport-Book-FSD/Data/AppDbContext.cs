using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TransportBookFSD.Models;

namespace TransportBookFSD.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Passenger-side entities 
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<PassengerProfile> PassengerProfiles { get; set; }
        public DbSet<DriverProfile> DriverProfiles { get; set; }
        public DbSet<PassengerCard> PassengerCards { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }

        // Staff
        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleRental> VehicleRentals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Booking>()
                .HasOne(b => b.Payment)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.PaymentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
