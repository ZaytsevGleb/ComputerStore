using FluentValidation.Results;

namespace BusinessLogic.Common.Exceptions;

public sealed class BadRequestException : Exception
{
    public string? Description { get; set; }

    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(ValidationResult validationResult) : base()
    {
        Description = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
    }
}
