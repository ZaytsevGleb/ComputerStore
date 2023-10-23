using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ComputerStore.Services.Auth.BusinessLogic.Errors;


[Serializable]
public sealed class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }
    
    public BadRequestException(string message, Exception exception) : base(message, exception)
    {
    }

    private BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}