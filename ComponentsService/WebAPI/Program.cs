using ComputerStore.Services.CS.Api.Constants;
using ComputerStore.Services.CS.BusinessLogic;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;
using WebApi.Middleware;
using WebAPI.Middleware;

namespace ComputerStore.Services.CS.Api;

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

        // Auth configuration
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.Authority = IdentityConstants.Uri;
                opt.Audience = IdentityConstants.Uri;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.FromSeconds(5)
                };
            });

        builder.Services.AddAuthorization(opt =>
        {
            opt.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "client_id"));
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
                    info: new OpenApiInfo { Title = "Components API", Version = "v1.0" }
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
            .UseMiddleware<RequestLoggingMiddleware>()
            .UseMiddleware<ExceptionMiddleware>()
            .UseCors()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
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