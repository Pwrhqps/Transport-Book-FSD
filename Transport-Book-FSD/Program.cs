using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Claims;
using TransportBookFSD.Components;
using TransportBookFSD.Data;
using TransportBookFSD.Models;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
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
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Staff2FA", policy =>
        policy.RequireRole("Staff")
              .RequireClaim("Staff2FA", "true"));
});

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

app.MapDefaultControllerRoute();

// ✅ LOGIN endpoint (HTTP POST, so cookie can be written)
app.MapPost("/auth/login-post", async (
    HttpContext http,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    IMemoryCache cache,
    ILogger<Program> logger,
    IWebHostEnvironment env) =>


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

    if (await userManager.IsInRoleAsync(user, "Staff"))
    {
        // generate 6-digit OTP
        var otp = Random.Shared.Next(0, 1000000).ToString("D6");

        cache.Set($"staff-otp:{user.Id}", otp, TimeSpan.FromMinutes(3));
        if (env.IsDevelopment())
        {
            logger.LogInformation("DEV STAFF OTP for {Email}: {Otp} (expires 3 mins)", email, otp);
        }


        // go to verify step
        return Results.Redirect("/auth/staff-verify");
    }

    if (await userManager.IsInRoleAsync(user, "Driver"))
        return Results.Redirect("/driver/home");

    if (await userManager.IsInRoleAsync(user, "Passenger"))
        return Results.Redirect("/passenger/home");



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

    return Results.Redirect("/staff/home");

}).DisableAntiforgery();

// ✅ LOGOUT endpoint (HTTP POST)
app.MapPost("/auth/logout", async (HttpContext http) =>
{
    await http.SignOutAsync(IdentityConstants.ApplicationScheme);
    return Results.Redirect("/");
}).DisableAntiforgery();

app.MapPost("/auth/staff-verify-post", async (
    HttpContext http,
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    IMemoryCache cache) =>
{
    var form = await http.Request.ReadFormAsync();
    var code = form["Code"].ToString();

    // must be logged in already (from step 1 login)
    var user = await userManager.GetUserAsync(http.User);
    if (user == null)
        return Results.Redirect("/auth/login");

    // must be staff
    if (!await userManager.IsInRoleAsync(user, "Staff"))
        return Results.Redirect("/auth/login");

    if (!cache.TryGetValue($"staff-otp:{user.Id}", out string otp))
        return Results.Redirect("/auth/staff-verify?error=1");

    if (otp != code)
        return Results.Redirect("/auth/staff-verify?error=1");

    // OTP correct -> remove it
    cache.Remove($"staff-otp:{user.Id}");

    // re-issue cookie with Staff2FA claim
    await signInManager.SignOutAsync();

    var claims = new List<Claim>
    {
        new Claim("Staff2FA", "true")
    };

    await signInManager.SignInWithClaimsAsync(user, isPersistent: false, claims);

    return Results.Redirect("/staff/home");
}).DisableAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbSeeder.SeedAsync(services);
}


app.Run();

