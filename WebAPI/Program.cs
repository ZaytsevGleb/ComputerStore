using BusinessLogic;
using FluentValidation;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;
using WebAPI.Middleware;

namespace WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Api configuration
        builder.Services
            .AddCors(opt => opt.AddDefaultPolicy(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()))
            .AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        // Register application dependencies
        builder.Services
            .AddBusinessLogicDependencies(builder.Configuration)
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddAutoMapper(Assembly.GetExecutingAssembly());

        // Swagger configuration
        builder.Services
            .AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc(
                    name: "v1.0",
                    info: new OpenApiInfo { Title = "ComputerStore API", Version = "v1.0" }
                );
            });

        builder.Host
            .ConfigureLogging((hostBuilder, loggingBuilder) =>
            {
                var logger = new LoggerConfiguration()
               .ReadFrom.Configuration(hostBuilder.Configuration)
               .Enrich.FromLogContext()
               .CreateLogger();

                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(logger);
            });


        var application = builder.Build();

        // Configure HTTP pipeline
        application
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("../swagger/v1.0/swagger.json", "API v1.0");
                options.RoutePrefix = "docs";
            })
            .UseMiddleware<ExceptionMiddleware>()
            .UseCors()
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGet(
                    pattern: "/",
                    requestDelegate: async context => await context.Response.WriteAsync("Ok"));

                endpoints.MapControllers();
            });

        await application.RunAsync();
    }
}