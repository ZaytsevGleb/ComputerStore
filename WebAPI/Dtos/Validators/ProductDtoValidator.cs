using FluentValidation;

namespace WebAPI.Dtos.Validators;

public sealed class ProductDtoValidator : AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Price).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Type).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}
