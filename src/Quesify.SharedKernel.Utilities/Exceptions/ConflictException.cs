namespace Quesify.SharedKernel.Utilities.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(
        string message = "",
        Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
