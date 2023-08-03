using System;
using System.Net.Http;
using System.Threading.Tasks;
using DataAccess.Context;
using Microsoft.Extensions.DependencyInjection;
using WebApi;
using Xunit;

namespace IntegrationTests.Infrastructure;

public abstract class IntegrationTest : IAsyncLifetime
{
    private TestingWebApplicationFactory<Program> _factory = default!;
    private IServiceProvider _serviceProvider = default!;

    protected HttpClient _httpClient
    {
        get; private set;
    }

    public async Task InitializeAsync()
    {
        _factory = new TestingWebApplicationFactory<Program>();
        _httpClient = _factory.CreateClient();
        _serviceProvider = _factory.Services;
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await db.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        _httpClient.Dispose();
        await _factory.DisposeAsync();
    }
}