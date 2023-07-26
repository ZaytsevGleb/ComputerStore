using FluentValidation;

namespace WebAPI.Dtos.Validators;

public sealed class ProducDtoValidator : AbstractValidator<ProductDto>
{
    public ProducDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Amount).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
        RuleFor(x => x.Type).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.CreatedDate).NotEmpty();
    }
}
