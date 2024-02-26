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
builder.Logging.SetMinimumLevel(LogLevel.Debug); // More detailed logs

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add MySQL support
var connectionString = builder.Configuration.GetConnectionString("FrybreadFusionContext");
builder.Services.AddDbContext<FrybreadFusionContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register the BlogPostRepository with IRepository<BlogPost>
builder.Services.AddScoped<IRepository<BlogPost>, BlogPostRepository>();

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<FrybreadFusionContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

// Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await SeedUsersAsync(userManager);

    var dbContext = services.GetRequiredService<FrybreadFusionContext>();
    dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
}

app.Run();

// Method for seeding users asynchronously
static async Task SeedUsersAsync(UserManager<AppUser> userManager)
{
    if (!userManager.Users.Any())
    {
        var adminUser = new AppUser
        {
            UserName = "admin",
            Email = "vextechmage@gmail.com",
            EmailConfirmed = true,
            FullName = "Joel Southall" // Assuming you've added FullName to AppUser
        };
        await userManager.CreateAsync(adminUser, "password"); // Use a stronger password in production
    }
}
