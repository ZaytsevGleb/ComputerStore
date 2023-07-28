using DataAccess.Entities;
using FluentValidation.TestHelper;
using WebAPI.Dtos;
using WebAPI.Dtos.Validators;
using Xunit;

namespace UnitTests.Products;

public sealed class ProductDtoValidatorTests
{
    private readonly ProducDtoValidator _validator;

    public ProductDtoValidatorTests()
    {
        _validator = new ProducDtoValidator();
    }

    [Fact]
    public void Validator_ShouldValidateProductDto()
    {
        // Arrange 
        var product = new ProductDto
        {
            Title = "Product",
            Price = 1.00m,
            Description = "Description",
            Type = ProductType.GPU,
            CreatedDate = DateTime.UtcNow
        };

        // Act/Assert
        _validator.TestValidate(product).ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_ShouldThrowTitleError()
    {
        // Arrange 
        var product = new ProductDto
        {
            Title = string.Empty,
            Price = 1.00m,
            Description = "Description",
            Type = ProductType.GPU,
            CreatedDate = DateTime.UtcNow
        };

        // Act  /Assert
        _validator.TestValidate(product).ShouldHaveValidationErrorFor(x => x.Title);
    }


    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Validator_ShouldThrowPriceError(decimal price)
    {
        // Arrange 
        var product = new ProductDto
        {
            Title = "Product",
            Price = price,
            Description = "Description",
            Type = ProductType.GPU,
            CreatedDate = DateTime.UtcNow
        };

        // Act/Assert
        _validator.TestValidate(product).ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Fact]
    public void Validator_ShouldThrowDescriptionError()
    {
        // Arrange 
        var product = new ProductDto
        {
            Title = "Product",
            Price = 20.00m,
            Description = string.Empty,
            Type = ProductType.GPU,
            CreatedDate = DateTime.UtcNow
        };

        // Act/Assert
        _validator.TestValidate(product).ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Validator_ShouldThrowTypeError()
    {
        // Arrange 
        var product = new ProductDto
        {
            Title = "Product",
            Price = 20.00m,
            Description = "Description",
            CreatedDate = DateTime.UtcNow
        };

        // Act/Assert
        _validator.TestValidate(product).ShouldHaveValidationErrorFor(x => x.Type);
    }

    [Fact]
    public void Validator_ShouldThrowCreatedDateError()
    {
        // Arrange 
        var product = new ProductDto
        {
            Title = "Product",
            Price = 20.00m,
            Description = "Description",
            Type = ProductType.Monitor
        };

        // Act/Assert
        _validator.TestValidate(product).ShouldHaveValidationErrorFor(x => x.CreatedDate);
    }
}
