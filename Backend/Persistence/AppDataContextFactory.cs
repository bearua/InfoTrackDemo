using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Persistence;

public class AppDataContextFactory : IDesignTimeDbContextFactory<AppDataContext>
{
    public AppDataContext CreateDbContext(string[] args)
    {
        var environment =
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            ?? "Development";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),"..","InfoTrackDemo"))
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile(
                $"appsettings.{environment}.json",
                optional: true)
            .Build();

        var connectionString =
            configuration.GetConnectionString("DefaultConnection");
        
        var optionsBuilder = new DbContextOptionsBuilder<AppDataContext>();
        
        optionsBuilder.UseNpgsql(connectionString);

        return new AppDataContext(optionsBuilder.Options);
    }
}