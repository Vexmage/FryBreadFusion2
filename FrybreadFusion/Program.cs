using FrybreadFusion.Data;
using FrybreadFusion.Data.Repositories;
using FrybreadFusion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using static FrybreadFusion.Data.SeedData;

var builder = WebApplication.CreateBuilder(args);


// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventSourceLogger();
builder.Logging.SetMinimumLevel(LogLevel.Debug);


// Add services to container.
builder.Services.AddControllersWithViews();


// Register the logger for SeedData
builder.Services.AddSingleton(typeof(ILogger<SeedDataLogger>), typeof(Logger<SeedDataLogger>));


// Add MySQL support
//var connectionString = builder.Configuration.GetConnectionString("FrybreadFusionContext");
var connectionString = builder.Configuration.GetConnectionString("MyDatabase");
builder.Services.AddDbContext<MyDatabase>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register the BlogPostRepository with IRepository<BlogPost>
builder.Services.AddScoped<IRepository<BlogPost>, FrybreadFusion.Data.Repositories.BlogPostRepository>();



builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 25;
})
.AddEntityFrameworkStores<MyDatabase>()
.AddDefaultTokenProviders();


// Configure application cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; 
    options.LogoutPath = "/Account/Logout"; 
    options.AccessDeniedPath = "/Account/AccessDenied"; 
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
    try
    {
        var logger = services.GetRequiredService<ILogger<SeedData>>();
        // Ensure the database is created and migrations are applied
        var dbContext = services.GetRequiredService<MyDatabase>();
        dbContext.Database.Migrate();

        // Now call SeedData.Initialize to seed the necessary data
        await SeedData.Initialize(services, logger);
        logger.LogInformation("Database seeding completed successfully.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();


