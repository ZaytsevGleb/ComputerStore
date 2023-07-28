using DataAccess;
using DataAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using WebAPI;

namespace WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            try
            {
                using var scope = host.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();

                await SeedData.AddSeedDataAsync(scope.ServiceProvider);
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "An error occurred seeding the DB.");
                await Log.CloseAndFlushAsync();
                throw;
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
             Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureLogging((hostBuilder, loggingBuilder) =>
                {
                    var logger = new LoggerConfiguration()
                   .ReadFrom.Configuration(hostBuilder.Configuration)
                   .Enrich.FromLogContext()
                   .CreateLogger();

                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddSerilog(logger);
                });
    }
}