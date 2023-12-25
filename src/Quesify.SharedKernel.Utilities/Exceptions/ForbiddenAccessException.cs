namespace Quesify.SharedKernel.Utilities.Exceptions;

public class ForbiddenAccessException : Exception
{
    public ForbiddenAccessException(
        string message = "",
        Exception? innerException = null)
        : base(message, innerException)
    {
    }
}
