using DataAccess.Context;
using Microsoft.Extensions.DependencyInjection;
using WebApi;
using Xunit;

namespace IntegrationTests.Infrastructure;

public abstract class IntegrationTest : IAsyncLifetime
{
    private TestingWebApplicationFactory<Program> _factory = default!;
    private IServiceProvider _serviceProvider = default!;

    protected HttpClientContext HttpClient { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        _factory = new TestingWebApplicationFactory<Program>();
        _serviceProvider = _factory.Services;
        HttpClient = new HttpClientContext(_factory.CreateClient(new() { AllowAutoRedirect = false }));

        using var scope = _serviceProvider.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await db.Database.EnsureCreatedAsync();
    }

    protected async Task AddSeedDataAsync<T>(T[] entities)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        foreach (var entity in entities)
        {
            await db.AddAsync(entity!);
        }

        await db.SaveChangesAsync();
    }

    protected async Task SaveDbChangesAsync(Func<ApplicationDbContext, Task> dbCommand)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbCommand(db);
        await db.SaveChangesAsync();
    }

    protected async Task CheckInDbAsync(Func<ApplicationDbContext, Task> predicate)
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await predicate(db);
    }

    public async Task DisposeAsync()
    {
        HttpClient.Dispose();
        await _factory.DisposeAsync();
    }
}
