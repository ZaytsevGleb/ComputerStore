using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Infrastructure;

public class ProdutsTests : IntegrationTest
{
    [Fact]
    public async Task GetProducts_ShouldReturnProducts()
    {
        var response = await _httpClient.GetAsync("api/products");
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
    }
}

