using FluentAssertions;
using IntegrationTests.Infrastructure;
using Xunit;

namespace IntegrationTests;

public sealed class ProductsTests : IntegrationTest
{
    [Fact]
    public async Task GetProducts_ShouldReturnProducts()
    {
        var response = await HttpClient.GetAsync("api/products");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        responseString.Should().NotBeNull();
    }
}
