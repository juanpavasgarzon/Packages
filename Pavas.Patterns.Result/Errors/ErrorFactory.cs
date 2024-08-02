using System.Net;

namespace Pavas.Patterns.Result.Errors;

public static class ErrorFactory
{
    public static Error CreateHttpError(HttpStatusCode statusCode, string name, string description)
    {
        return new Error
        {
            Code = statusCode.ToString(),
            Name = name,
            Description = description,
            Extensions = { ["Status"] = statusCode }
        };
    }

    public static Error CreateSystemError(string code, string name, string description)
    {
        return new Error
        {
            Code = code,
            Name = name,
            Description = description
        };
    }
}