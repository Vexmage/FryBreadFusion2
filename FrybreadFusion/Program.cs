using FrybreadFusion.Data;
using FrybreadFusion.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using FrybreadFusion.Data.Repositories;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Register your IRepository implementation
builder.Services.AddScoped<IRepository<BlogPost>, BlogPostRepository>();


// Add DbContext with your connection string
builder.Services.AddDbContext<MyDatabase>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("MyDatabase"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MyDatabase"))));

// Identity configuration
builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<MyDatabase>()
    .AddDefaultTokenProviders();


// Logging configuration
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventSourceLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Lets add some custom security headers in the middleware pipeline as per the OWASP Secure Headers fixes we've identified that we need
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self'; object-src 'none'; frame-ancestors 'none';");
    context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    // we may need more if we want to solve all the vulnerability issues

    await next();
});


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Make sure your DbContext is correctly set up here, if you're using one
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        // Ensure logger is correct and available
        var logger = services.GetRequiredService<ILogger<SeedData>>();
        // Migrate database to latest version
        var dbContext = services.GetRequiredService<MyDatabase>(); // Make sure this matches your DbContext type
        dbContext.Database.Migrate();
        // Now correctly call Initialize with both required parameters
        await SeedData.Initialize(services, logger);
    }
    catch (Exception ex)
    {
        // Use logger to log startup errors
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}


app.Run();
