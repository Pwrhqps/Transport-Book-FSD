using Microsoft.AspNetCore.Identity;
using TransportBookFSD.Models;

namespace TransportBookFSD.Data
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

            // ✅ Staff seeded
            var staffEmail = "staff@transport.com";
            var staffPassword = "Staff1234!";

            var staffUser = await userMgr.FindByEmailAsync(staffEmail);

            if (staffUser == null)
            {
                staffUser = new ApplicationUser
                {
                    UserName = staffEmail,
                    Email = staffEmail,
                    EmailConfirmed = true
                };

                var createResult = await userMgr.CreateAsync(staffUser, staffPassword);

                // If create failed, stop silently to avoid crashing startup
                if (!createResult.Succeeded)
                    return;
            }

            // ✅ Always ensure Staff role is assigned (even if user already existed)
            if (!await userMgr.IsInRoleAsync(staffUser, "Staff"))
            {
                await userMgr.AddToRoleAsync(staffUser, "Staff");
            }
        }
    }
}
