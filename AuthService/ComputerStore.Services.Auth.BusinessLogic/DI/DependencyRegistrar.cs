using ComputerStore.Services.Auth.BusinessLogic.Abstractions;
using ComputerStore.Services.Auth.BusinessLogic.Infrastructure;
using ComputerStore.Services.Auth.BusinessLogic.Infrastructure.Options;
using ComputerStore.Services.Auth.BusinessLogic.Services;
using ComputerStore.Services.Auth.DataAccess.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerStore.Services.Auth.BusinessLogic.DI;
public static class DependencyRegistrar
{
    public static void AddBusinessLogicDependencies(this IServiceCollection services, IConfiguration config)
    {

        services.Configure<JwtOptions>(config.GetSection(nameof(JwtOptions)));
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddDataAccessDependencies(config);
    }
}
