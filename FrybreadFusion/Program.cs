using FrybreadFusion.Data;
using FrybreadFusion.Data.Repositories;
using FrybreadFusion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);


// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventSourceLogger();
builder.Logging.SetMinimumLevel(LogLevel.Debug); // more detailed logs


// Add services to container.
builder.Services.AddControllersWithViews();

// Add MySQL support
var connectionString = builder.Configuration.GetConnectionString("FrybreadFusionContext");
builder.Services.AddDbContext<FrybreadFusionContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register the BlogPostRepository with IRepository<BlogPost>
builder.Services.AddScoped<IRepository<BlogPost>, FrybreadFusion.Data.Repositories.BlogPostRepository>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    // Password settings, user settings, etc., can be configured here as well
})
    .AddEntityFrameworkStores<FrybreadFusionContext>()
    .AddDefaultTokenProviders();

// Configure application cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Your login path
    options.LogoutPath = "/Account/Logout"; // Your logout path
    options.AccessDeniedPath = "/Account/AccessDenied"; // Your access denied path
});


var app = builder.Build();

// Configure HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<FrybreadFusion.Data.SeedData.SeedDataLogger>>();
    try
    {
        await SeedData.Initialize(services, logger);
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred seeding the Database.");
    }
}

app.Run();



// Method for seeding users asynchronously -- async methods are fun and I've worked with
// them befor
// e, so I wanted to try it out here.  We'll see how it goes! 
/*
static async Task SeedDataAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    // Ensure the Admin role exists
    await EnsureRolesAsync(roleManager);

    // Seed admin user and ensure they are in the Admin role
    string adminEmail = "admin@example.com"; // Will need to replace with real admin email
    string adminPassword = "AdminPassword123!"; // production version will need stronger passwd
    if (!userManager.Users.Any(user => user.Email == adminEmail))
    {
        var adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
        var userResult = await userManager.CreateAsync(adminUser, adminPassword);
        if (userResult.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
*/
// Method for ensuring roles exist asynchronously
/*
static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
{
    var adminRoleName = "Admin"; // Name of the admin role
    var roleExists = await roleManager.RoleExistsAsync(adminRoleName); // Check if the Admin role exists
    if (!roleExists) // If the Admin role doesn't exist, create it
    {
        await roleManager.CreateAsync(new IdentityRole(adminRoleName)); // Create the Admin role
    }
}
*/


