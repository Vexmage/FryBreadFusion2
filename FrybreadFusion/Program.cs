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

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<FrybreadFusionContext>()
    .AddDefaultTokenProviders();

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


// Seed Data -- interesting figuring this out. 
// It seems better than putting my seed data all in the DbContext.cs file,
// based on some of my reading.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    await SeedUsersAsync(userManager);

    var dbContext = services.GetRequiredService<FrybreadFusionContext>();
    // Ensure the database is created
    dbContext.Database.EnsureCreated();
    // Apply any pending migrations
    dbContext.Database.Migrate();
}

app.Run();



// Method for seeding users asynchronously -- async methods are fun and I've worked with
// them before, so I wanted to try it out here.  We'll see how it goes! 
static async Task SeedUsersAsync(UserManager<IdentityUser> userManager)
{
    if (!userManager.Users.Any())
    {
        var adminUser = new IdentityUser
        {
            UserName = "admin", 
            Email = "vextechmage@gmail.com",
            EmailConfirmed = true
        };
        await userManager.CreateAsync(adminUser, "password"); // Replace with a stronger password in production
        // Add roles or other user-related data later, once we understand more about those
    }
}