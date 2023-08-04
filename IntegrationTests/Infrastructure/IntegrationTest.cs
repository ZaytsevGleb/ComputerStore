using DataAccess.Context;
using Microsoft.Extensions.DependencyInjection;
using WebApi;
using Xunit;

namespace IntegrationTests.Infrastructure;

public abstract class IntegrationTest : IAsyncLifetime
{
    private TestingWebApplicationFactory<Program> _factory = default!;
    private IServiceProvider _serviceProvider = default!;

    protected HttpClient HttpClient { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        _factory = new TestingWebApplicationFactory<Program>();
        _serviceProvider = _factory.Services;
        HttpClient = _factory.CreateClient(new() { AllowAutoRedirect = false });

        using var scope = _serviceProvider.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await db.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        HttpClient.Dispose();
        await _factory.DisposeAsync();
    }
}
