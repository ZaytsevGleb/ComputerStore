using DataAccess;
using DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using WebAPI;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host
              .CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>())
              .Build();

            try
            {
                using var scope = host.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();

                await SeedData.AddSeedDataAsync(scope.ServiceProvider);
            }
            catch (Exception ex)
            {
            }

            await host.RunAsync();
        }
    }
}