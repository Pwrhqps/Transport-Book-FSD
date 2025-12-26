using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Transport_Book_FSD.Models;

namespace Transport_Book_FSD.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Passenger-side entities (we’ll create these classes next)
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<PassengerProfile> PassengerProfiles { get; set; }
        public DbSet<DriverProfile> DriverProfiles { get; set; }
    }
}
