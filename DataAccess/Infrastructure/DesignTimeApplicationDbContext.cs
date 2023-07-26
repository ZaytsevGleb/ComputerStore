using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Infrastructure;

public class DesignTimeApplicationDbContext : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../WebApi/"))
            .AddJsonFile("appsettings.json")
            .Build();

        var options = new DbContextOptionsBuilder<ApplicationDbContext>();
        options.UseNpgsql(config.GetConnectionString("DefaultConnection"));

        return new ApplicationDbContext(options.Options);
    }
}
