using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Transport_Book_FSD.Components;
using Transport_Book_FSD.Data;
using Transport_Book_FSD.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connStr);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddHttpContextAccessor();

// ✅ Authorization + Blazor auth state
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

// ✅ LOGIN endpoint (HTTP POST, so cookie can be written)
app.MapPost("/auth/login-post", async (
    HttpContext http,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager) =>
{
    var form = await http.Request.ReadFormAsync();
    var email = form["Email"].ToString();
    var password = form["Password"].ToString();

    var result = await signInManager.PasswordSignInAsync(
        email, password, isPersistent: false, lockoutOnFailure: false);

    if (!result.Succeeded)
        return Results.Redirect("/auth/login?error=1");

    var user = await userManager.FindByEmailAsync(email);
    if (user == null)
        return Results.Redirect("/auth/login?error=1");

    if (await userManager.IsInRoleAsync(user, "Passenger"))
        return Results.Redirect("/passenger/home");

    if (await userManager.IsInRoleAsync(user, "Driver"))
        return Results.Redirect("/driver/home");

    if (await userManager.IsInRoleAsync(user, "Staff"))
        return Results.Redirect("/staff");

    return Results.Redirect("/");
}).DisableAntiforgery();

// ✅ STAFF LOGIN endpoint (HTTP POST) - only Staff can login here
app.MapPost("/auth/staff-login-post", async (
    HttpContext http,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager) =>
{
    var form = await http.Request.ReadFormAsync();
    var email = form["Email"].ToString();
    var password = form["Password"].ToString();

    var user = await userManager.FindByEmailAsync(email);
    if (user == null)
        return Results.Redirect("/auth/staff-login?error=1");

    if (!await userManager.IsInRoleAsync(user, "Staff"))
        return Results.Redirect("/auth/staff-login?notstaff=1");

    var result = await signInManager.PasswordSignInAsync(
        email, password, isPersistent: false, lockoutOnFailure: false);

    if (!result.Succeeded)
        return Results.Redirect("/auth/staff-login?error=1");

    return Results.Redirect("/staff");
}).DisableAntiforgery();

// ✅ LOGOUT endpoint (HTTP POST)
app.MapPost("/auth/logout", async (HttpContext http) =>
{
    await http.SignOutAsync(IdentityConstants.ApplicationScheme);
    return Results.Redirect("/");
}).DisableAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Seed 1 default staff account if none exists
    if (!db.Staffs.Any())
    {
        db.Staffs.Add(new Staff
        {
            Email = "staff1@gmail.com",
            Password = "password123"
        });

        await db.SaveChangesAsync();
    }

    await DbSeeder.SeedAsync(scope.ServiceProvider);
}

app.Run();

