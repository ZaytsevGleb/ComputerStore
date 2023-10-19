using ComputerStore.Services.Auth.Api.Mapper;
using ComputerStore.Services.Auth.BusinessLogic.DI;
using System.Reflection;
using ComputerStore.Services.Auth.Api.Middleware;

namespace ComputerStore.Services.Auth.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddBusinessLogicDependencies(builder.Configuration);

        builder.Services.AddAutoMapper(typeof(ApiProfile).GetTypeInfo().Assembly);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        ExceptionMiddlewareExtensions.UseExceptionHandler(app);
        app.UseRequestLoggingMiddleware();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
