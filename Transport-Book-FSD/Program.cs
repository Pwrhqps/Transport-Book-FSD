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

builder.Services.AddHttpContextAccessor();   // ✅ important

builder.Services.AddAuthorization();

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

    return Results.Redirect("/");
}).DisableAntiforgery(); // simplest for now (you can enable later)

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    await DbSeeder.SeedAsync(scope.ServiceProvider);
}

app.Run();
