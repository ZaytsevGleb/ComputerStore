using FluentAssertions;
using IntegrationTests.Infrastructure;
using WebAPI.Dtos;
using Xunit;

namespace IntegrationTests;

public sealed class ProductsTests : IntegrationTest
{
    [Fact]
    public async Task GetProduct_ShouldReturnProduct()
    {
        var productDto = await HttpClient.GetAsync<ProductDto>("api/products/249c7e5c-caa7-4786-8865-7558cf439cbc");

        productDto.Should().NotBeNull();
    }

    [Fact]
    public async Task GetProducts_ShouldReturnProducts()
    {
        var productDtos = await HttpClient.GetAllAsync<ProductDto>("api/products");
        productDtos.Should().NotBeNull();
    }
}
