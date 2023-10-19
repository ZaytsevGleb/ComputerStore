using System.Runtime.Serialization;

namespace ComputerStore.Services.Auth.BusinessLogic.Errors;

[Serializable]
public class NotFoundException: Exception
{
    public NotFoundException(string message, object key)
    :base($"Entity \"{message}\" by ({key}) was not found.")
    {
    }
    
    private NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}