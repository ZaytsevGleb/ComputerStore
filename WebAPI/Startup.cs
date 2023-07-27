using BusinessLogic;
using DataAccess;
using FluentValidation;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using WebAPI.Middleware;

namespace WebAPI;

public sealed class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Register application dependencies
        services
            .AddBusinessLogicDependencies()
            .AddDataAccessDependencies(_configuration)
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Api configuration
        services
            .AddCors(opt => opt.AddDefaultPolicy(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()))
            .AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        // Swagger configuration
        services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc(
                name: "v1.0",
                info: new OpenApiInfo { Title = "ComputerStore API", Version = "v1.0" }
            );
        });
    }

    public void Configure(IApplicationBuilder builder)
    {
        builder
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../swagger/v1.0/swagger.json", "API v1.0");
                options.RoutePrefix = "docs";
            })
            .UseMiddleware<ErrorHandlingMiddleware>()
            .UseCors()
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGet(
                    pattern: "/",
                    requestDelegate: async context => await context.Response.WriteAsync("0_o"));

                endpoints.MapControllers();
            });
    }
}
