using DataAccess.Entities;
using FluentAssertions;
using IntegrationTests.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using WebAPI.Dtos;
using Xunit;

namespace IntegrationTests;

public sealed class ProductsTests : IntegrationTest
{
    private static readonly Product[] products = SeedData.GetProducts();

    [Fact]
    public async Task GetProduct_ShouldReturnProduct()
    {
        await AddSeedDataAsync(products);
        var product = products[0];

        var productDto = await HttpClient.GetAsync<ProductDto>(UriConstants.Products, products[0].Id);
        productDto!.Description.Should().Be(product.Description);
        productDto.Title.Should().Be(product.Title);
        productDto.Price.Should().Be(product.Price);
        productDto.Type.Should().Be(product.Type);
    }

    [Fact]
    public async Task GetProduct_InvalidId_ShouldReturnNothing()
    {
        await AddSeedDataAsync(products);

        var productDto = await HttpClient.GetAsync<ProductDto>(UriConstants.Products, Guid.Empty);
        productDto.Should().BeNull();
    }

    [Fact]
    public async Task GetProducts_ShouldReturnProducts()
    {
        await AddSeedDataAsync(products);
        var productDtos = await HttpClient.GetAllAsync<ProductDto>(UriConstants.Products, "GB");
        productDtos.Count().Should().Be(2);
    }

    [Fact]
    public async Task CreateProduct_ShouldCreate()
    {
        var product = SeedData.Product;

        await HttpClient.CreateAsync(UriConstants.Products, product);

        await CheckInDbAsync(async db =>
        {
            var existProduct = await db.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            existProduct!.Price.Should().Be(product.Price);
            existProduct.Title.Should().Be(product.Title);
            existProduct.Description.Should().Be(product.Description);
            existProduct.Type.Should().Be(product.Type);
        });
    }

    [Fact]
    public async Task CreateProduct_ExistProduct_ShouldDoesntCreateProduct()
    {
        await AddSeedDataAsync(products);
        var product = products.First();

        var result = await HttpClient.CreateAsync(UriConstants.Products, product);
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateProduct_ShouldUpdate()
    {
        ///TODO remove id from controller
        ///TODO Create IsSuccessResponse extension method
        ///TODO thinkig about interceptors and validator with create modified dates
        var id = products[0].Id;
        await AddSeedDataAsync(products);

        var updatedProduct = new Product
        {
            Description = "Desc",
            Price = 210,
            Title = "Title",
            Type = ProductType.SSD
        };

        await HttpClient.UpdateAsync(UriConstants.Products, id, updatedProduct);

        await CheckInDbAsync(async db =>
        {
            var updartedProduct = await db.Products.FirstOrDefaultAsync(x => x.Id == id);
            updartedProduct!.Description.Should().Be(updatedProduct.Description);
            updartedProduct.Title.Should().Be(updatedProduct.Title);
            updartedProduct.Price.Should().Be(updatedProduct.Price);
            updartedProduct.Type.Should().Be(updatedProduct.Type);
        });
    }

    [Fact]
    public async Task DeleteProduct_ShouldDelete()
    {
        await AddSeedDataAsync(products);
        var product = products.First();

        await HttpClient.DeleteAsync(UriConstants.Products, product.Id);

        await CheckInDbAsync(async db =>
        {
            var deletedProduct = await db.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            deletedProduct.Should().BeNull();
        });
    }
}
