﻿namespace Quesify.SharedKernel.Utilities.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(
        string? message = null,
        Exception? innerException = null)
        : base(message, innerException)
    {
    }
}