using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Transport_Book_FSD.Models;

namespace Transport_Book_FSD.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userMgr = services.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Passenger", "Driver", "Staff" };

            foreach (var r in roles)
            {
                if (!await roleMgr.RoleExistsAsync(r))
                    await roleMgr.CreateAsync(new IdentityRole(r));
            }

            // Staff seeded only
            var staffEmail = "staff@transport.com";
            var staffUser = await userMgr.FindByEmailAsync(staffEmail);

            if (staffUser == null)
            {
                staffUser = new ApplicationUser
                {
                    UserName = staffEmail,
                    Email = staffEmail
                };

                await userMgr.CreateAsync(staffUser, "Staff1234!");
                await userMgr.AddToRoleAsync(staffUser, "Staff");
            }
        }
    }
}
