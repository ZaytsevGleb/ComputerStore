using FluentValidation;

namespace WebAPI.Dtos.Validators;

public sealed class OrderDtoValidator : AbstractValidator<OrderDto>
{
    public OrderDtoValidator()
    {
        RuleFor(x => x.UserID).NotEmpty();
        RuleFor(x => x.Status).NotEmpty();
        RuleFor(x => x.CreatedDate).NotEmpty();
    }
}
