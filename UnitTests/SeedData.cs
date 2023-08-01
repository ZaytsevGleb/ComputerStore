using Shared.Enums;
using WebAPI.Dtos;

namespace UnitTests;

public static class SeedData
{
    public static ProductDto GetProductDto() => new()
    {
        Title = "Product",
        Price = 1.00m,
        Description = "Description",
        Type = ProductType.GPU,
        CreatedDate = DateTime.Now
    };

    public static ProductDto GetProductDtoWithoutTitle() => new()
    {
        Title = string.Empty,
        Price = 1.00m,
        Description = "Description",
        Type = ProductType.GPU,
        CreatedDate = DateTime.UtcNow
    };

    public static ProductDto GetProductDtoCustomPrice(decimal price) => new()
    {
        Title = "Product",
        Price = price,
        Description = "Description",
        Type = ProductType.GPU,
        CreatedDate = DateTime.UtcNow
    };

    public static ProductDto GetProductDtoWithoutDescription() => new()
    {
        Title = "Product",
        Price = 20.00m,
        Description = string.Empty,
        Type = ProductType.GPU,
        CreatedDate = DateTime.UtcNow
    };

    public static ProductDto GetProductWithoutType() => new()
    {
        Title = "Product",
        Price = 20.00m,
        Description = "Description",
        CreatedDate = DateTime.UtcNow
    };

    public static ProductDto GetProductDtoWithoutCreatedDate() => new()
    {
        Title = "Product",
        Price = 20.00m,
        Description = "Description",
        Type = ProductType.Monitor
    };
}
