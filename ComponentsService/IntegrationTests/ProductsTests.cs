using ComputerStore.Services.CS.DataAccess.Entities;
using ComputerStore.Services.CS.IntegrationTests.Infrastructure;
using ComputerStore.Services.CS.Shared.Enums;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebAPI.Dtos;
using Xunit;

namespace ComputerStore.Services.CS.IntegrationTests;

public sealed class ProductsTests : IntegrationTest
{
    private static readonly Product[] products = SeedData.GetProducts();

    [Fact]
    public async Task GetProduct_ShouldReturnProduct()
    {
        await AddSeedDataAsync(products);
        var product = products[0];

        var productDto = await HttpClient.GetAsync<ProductDto>(UriConstants.Products, product.Id);
        productDto.Should().BeEquivalentTo(product);
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
        var expectedProducts = products.Where(x => x.Title.Contains("GB")).ToList();

        await AddSeedDataAsync(products);
        var productDtos = await HttpClient.GetAllAsync<ProductDto>(UriConstants.Products, "GB");

        productDtos!.Should().BeEquivalentTo(expectedProducts);
    }

    [Fact]
    public async Task CreateProduct_ShouldCreate()
    {
        var product = SeedData.Product;

        await HttpClient.CreateAsync(UriConstants.Products, product);

        await CheckInDbAsync(async db =>
        {
            var existingProduct = await db.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            existingProduct!.Price.Should().Be(product.Price);
            existingProduct.Title.Should().Be(product.Title);
            existingProduct.Description.Should().Be(product.Description);
            existingProduct.Type.Should().Be(product.Type);
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
        await AddSeedDataAsync(products);
        var product = products[0];

        product.Description = "Desc";
        product.Price = 210;
        product.Title = "Title";
        product.Type = ProductType.SSD;

        await HttpClient.UpdateAsync(UriConstants.Products, product.Id, product);

        await CheckInDbAsync(async db =>
        {
            var updatedProduct = await db.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
            updatedProduct!.Description.Should().Be(product.Description);
            updatedProduct.Title.Should().Be(product.Title);
            updatedProduct.Price.Should().Be(product.Price);
            updatedProduct.Type.Should().Be(product.Type);
        });
    }

    [Fact]
    public async Task UpdateProduct_NewProduct_ShouldDoesntUpdateProduct()
    {
        await AddSeedDataAsync(products);

        var invalidProduct = products[0];
        invalidProduct.Id = Guid.NewGuid();

        var result = await HttpClient.UpdateAsync(UriConstants.Products, invalidProduct.Id, invalidProduct);

        result.Should().BeNull();
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
