namespace Blaze4.Application.Exceptions;

public class InvalidGameOperationException : Exception
{
    public InvalidGameOperationException(string message) : base(message)
    {
    }
}