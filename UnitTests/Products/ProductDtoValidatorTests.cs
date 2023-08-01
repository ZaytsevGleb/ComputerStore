using FluentValidation.TestHelper;
using WebAPI.Dtos.Validators;
using Xunit;

namespace UnitTests.Products;

public class ProductDtoValidatorTests
{
    private readonly ProducDtoValidator _validator;

    public ProductDtoValidatorTests()
    {
        _validator = new ProducDtoValidator();
    }

    [Fact]
    public void Validator_ShouldValidateProductDto()
    {
        // Arrange/Act/Assert
        _validator.TestValidate(SeedData.GetProductDto())
            .ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validator_ShouldThrowTitleError()
    {
        // Arrange/Act/Assert
        _validator.TestValidate(SeedData.GetProductDtoWithoutTitle())
            .ShouldHaveValidationErrorFor(x => x.Title);
    }


    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Validator_ShouldThrowPriceError(decimal price)
    {
        // Arrange/Act/Assert
        _validator.TestValidate(SeedData.GetProductDtoCustomPrice(price))
            .ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Fact]
    public void Validator_ShouldThrowDescriptionError()
    {
        // Arrange/Act/Assert
        _validator.TestValidate(SeedData.GetProductDtoWithoutDescription())
            .ShouldHaveValidationErrorFor(x => x.Description);
    }

    [Fact]
    public void Validator_ShouldThrowTypeError()
    {
        // Arrange/Act/Assert
        _validator.TestValidate(SeedData.GetProductWithoutType())
            .ShouldHaveValidationErrorFor(x => x.Type);
    }

    [Fact]
    public void Validator_ShouldThrowCreatedDateError()
    {
        // Arrange/Act/Assert
        _validator.TestValidate(SeedData.GetProductDtoWithoutCreatedDate())
            .ShouldHaveValidationErrorFor(x => x.CreatedDate);
    }
}
