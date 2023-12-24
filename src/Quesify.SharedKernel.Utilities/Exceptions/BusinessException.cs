﻿namespace Quesify.SharedKernel.Utilities.Exceptions;

public class BusinessException : Exception
{
    public object? Errors { get; set; }

    public BusinessException(
        string? message = null,
        Exception? innerException = null,
        object? errors = null)
        : base(message, innerException)
    {
        Errors = errors;
    }
}
