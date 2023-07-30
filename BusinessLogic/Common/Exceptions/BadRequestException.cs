using FluentValidation.Results;
using System.Runtime.Serialization;

namespace BusinessLogic.Common.Exceptions;

[Serializable]
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

    private BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
