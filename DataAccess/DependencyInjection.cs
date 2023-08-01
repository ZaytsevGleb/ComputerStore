using DataAccess.Context;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseNpgsql(connectionString);
                options.AddInterceptors(provider.GetRequiredService<SetupDateInterceptor>());
            });

        services
            .AddSingleton<SetupDateInterceptor>();

        services
            .AddTransient(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}
