using FrybreadFusion.Data;
using FrybreadFusion.Data.Repositories;
using FrybreadFusion.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddEventSourceLogger();
builder.Logging.SetMinimumLevel(LogLevel.Debug); // Setting LogLevel to Debug for more detailed logs


// Add services to container.
builder.Services.AddControllersWithViews();

// Add MySQL support
var connectionString = builder.Configuration.GetConnectionString("FrybreadFusionContext");
builder.Services.AddDbContext<FrybreadFusionContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register the BlogPostRepository with IRepository<BlogPost>
builder.Services.AddScoped<IRepository<BlogPost>, FrybreadFusion.Data.Repositories.BlogPostRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Let's get that seed info in there!
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FrybreadFusionContext>();
    // Ensure the database is created
    dbContext.Database.EnsureCreated();

    // This checks for any pending migrations and applies them.
    dbContext.Database.Migrate();

}

app.Run();
