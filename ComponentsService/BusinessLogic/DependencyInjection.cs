using System.Reflection;
using ComputerStore.Services.CS.BusinessLogic.Interfaces;
using ComputerStore.Services.CS.BusinessLogic.Services;
using ComputerStore.Services.CS.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerStore.Services.CS.BusinessLogic;

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
