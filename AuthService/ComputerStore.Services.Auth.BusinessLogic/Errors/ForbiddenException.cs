using System.Runtime.Serialization;

namespace ComputerStore.Services.Auth.BusinessLogic.Errors;

[Serializable]
public class ForbiddenException : Exception
{
    public ForbiddenException(string name, object key) 
        : base($"Access to Entity \"{name}\" ({key}) is forbidden.")
    {
    }
    
    public ForbiddenException(string name) 
        : base($"Access to Entity \"{name}\" is forbidden.")
    {
    }
    
    private ForbiddenException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}