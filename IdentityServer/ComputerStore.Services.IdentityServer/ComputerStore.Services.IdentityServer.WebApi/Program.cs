using ComputerStore.Services.IdentityServer.WebApi.Configuration;
using ComputerStore.Services.IdentityServer.WebApi.Data;
using ComputerStore.Services.IdentityServer.WebApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Datebase configuration
builder.Services.AddDbContext<AuthDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User, Role>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 4;
})
.AddEntityFrameworkStores<AuthDbContext>()
.AddDefaultTokenProviders();

// IdentityServer configuration
builder.Services.AddIdentityServer()
    .AddInMemoryApiResources(ISConfiguration.ApiResources)
    .AddInMemoryIdentityResources(ISConfiguration.IdentityResources)
    .AddInMemoryApiScopes(ISConfiguration.ApiScopes)
    .AddInMemoryClients(ISConfiguration.Clients)
    .AddDeveloperSigningCredential();

builder.Services.ConfigureApplicationCookie(cfg =>
{
    cfg.Cookie.Name = "ComputerStore.Identity.Cookie";
    cfg.LoginPath = "/Auth/Login";
    cfg.LogoutPath = "/Auth/Logout";

});

builder.Services.AddHttpClient();
builder.Services
    .AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseIdentityServer();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
