using Microsoft.AspNetCore.Identity;

namespace Transport_Book_FSD.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Custom fields for Drivers
        public string? VehicleType { get; set; }
        public string? LicensePlate { get; set; }

        // Note: IdentityUser already includes a 'PhoneNumber' property, 
        // so you don't need to add it here.
    }
}