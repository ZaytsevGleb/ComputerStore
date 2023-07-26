using BusinessLogic.Common.Interfaces;
using BusinessLogic.Common.Services;
using BusinessLogic.Products.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection services)
    {
        services
            .AddAutoMapper(Assembly.GetExecutingAssembly());

        services
            .AddScoped<IProductsService, ProductService>();
        services
            .AddSingleton<IDateTimeService, DateTimeService>();

        return services;
    }
}
