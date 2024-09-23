using System.Net;

namespace Pavas.Patterns.Result.Errors;

/// <summary>
/// Provides factory methods for creating <see cref="Error"/> instances, such as HTTP errors, system errors, and errors from exceptions.
/// </summary>
public static class ErrorFactory
{
    /// <summary>
    /// Creates an <see cref="Error"/> instance representing an HTTP error.
    /// </summary>
    /// <param name="statusCode">The HTTP status code associated with the error.</param>
    /// <param name="name">A short, descriptive name for the error.</param>
    /// <param name="description">A detailed description of the error.</param>
    /// <returns>An <see cref="Error"/> instance containing the HTTP error details, including status code and additional metadata.</returns>
    public static Error CreateHttpError(HttpStatusCode statusCode, string name, string description)
    {
        return new Error
        {
            Code = statusCode.ToString(),
            Name = name,
            Description = description,
            Extensions = { ["Status"] = (int)statusCode }
        };
    }

    /// <summary>
    /// Creates an <see cref="Error"/> instance representing a system error.
    /// </summary>
    /// <param name="code">The error code for the system error.</param>
    /// <param name="name">A short, descriptive name for the error.</param>
    /// <param name="description">A detailed description of the error.</param>
    /// <returns>An <see cref="Error"/> instance containing the system error details.</returns>
    public static Error CreateSystemError(string code, string name, string description)
    {
        return new Error
        {
            Code = code,
            Name = name,
            Description = description
        };
    }

    /// <summary>
    /// Creates an <see cref="Error"/> instance from an exception.
    /// </summary>
    /// <param name="exception">The exception from which to create the error.</param>
    /// <returns>An <see cref="Error"/> instance containing details extracted from the exception, such as its type, method name, and message.</returns>
    public static Error FromException(Exception exception)
    {
        return new Error
        {
            Code = exception.GetType().Name,
            Name = exception.TargetSite?.Name ?? exception.GetType().Name,
            Description = exception.Message
        };
    }
}