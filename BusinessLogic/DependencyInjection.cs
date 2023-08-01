using BusinessLogic.Common.Interfaces;
using BusinessLogic.Products.Services;
using DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BusinessLogic;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDataAccessDependencies(configuration);

        services
            .AddAutoMapper(Assembly.GetExecutingAssembly());

        services
            .AddScoped<IProductsService, ProductService>();

        return services;
    }
}
