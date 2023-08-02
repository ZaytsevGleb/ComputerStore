using System.Runtime.Serialization;

namespace BusinessLogic.Exceptions;

[Serializable]
public sealed class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" by ({key}) was not found.")
    {
    }

    private NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
