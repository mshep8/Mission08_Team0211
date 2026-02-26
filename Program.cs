using Microsoft.EntityFrameworkCore;
using Mission08_Team0211.Models;

LoadDotEnv();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var postgresConnection = BuildConnectionStringFromEnv();
var connectionString = string.IsNullOrWhiteSpace(postgresConnection)
    ? builder.Configuration.GetConnectionString("QuadrantsConnection")
    : postgresConnection;

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("No PostgreSQL connection string configured. Set PG* values in .env or ConnectionStrings:QuadrantsConnection.");
}

builder.Services.AddDbContext<QuadrantsContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IQuadrantsRepository, EFQuadrantsRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<QuadrantsContext>();
    context.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

static void LoadDotEnv(string path = ".env")
{
    if (!File.Exists(path))
    {
        return;
    }

    foreach (var rawLine in File.ReadAllLines(path))
    {
        var line = rawLine.Trim();

        if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
        {
            continue;
        }

        var splitIndex = line.IndexOf('=');
        if (splitIndex <= 0)
        {
            continue;
        }

        var key = line[..splitIndex].Trim();
        var value = line[(splitIndex + 1)..].Trim().Trim('"');

        if (!string.IsNullOrWhiteSpace(key))
        {
            Environment.SetEnvironmentVariable(key, value);
        }
    }
}

static string? BuildConnectionStringFromEnv()
{
    var host = Environment.GetEnvironmentVariable("PGHOST");
    var port = Environment.GetEnvironmentVariable("PGPORT") ?? "5432";
    var database = Environment.GetEnvironmentVariable("PGDATABASE");
    var username = Environment.GetEnvironmentVariable("PGUSER");
    var password = Environment.GetEnvironmentVariable("PGPASSWORD");

    if (string.IsNullOrWhiteSpace(host) ||
        string.IsNullOrWhiteSpace(database) ||
        string.IsNullOrWhiteSpace(username) ||
        string.IsNullOrWhiteSpace(password))
    {
        return null;
    }

    return $"Host={host};Port={port};Database={database};Username={username};Password={password}";
}
