using System.Runtime.CompilerServices;

namespace Pavas.Patterns.Result.Errors;

/// <summary>
/// Represents a structured error with a code, name, description, and optional additional metadata (extensions).
/// </summary>
public sealed record Error
{
    /// <summary>
    /// Gets the error code, typically used to categorize the type of error.
    /// </summary>
    /// <value>A string representing the error code.</value>
    public required string Code { get; init; }

    /// <summary>
    /// Gets the short, descriptive name of the error.
    /// </summary>
    /// <value>A string representing the name of the error.</value>
    public required string Name { get; init; }

    /// <summary>
    /// Gets the detailed description of the error, providing more context.
    /// </summary>
    /// <value>A string representing the description of the error.</value>
    public required string Description { get; init; }

    /// <summary>
    /// Gets additional metadata or context for the error in the form of key-value pairs.
    /// </summary>
    /// <value>A dictionary of key-value pairs, where the key is a string and the value is an object, representing additional error information.</value>
    public Dictionary<string, object?> Extensions { get; init; } = new(StringComparer.Ordinal);

    /// <summary>
    /// Returns a string representation of the error, including its code, name, description, and any extensions.
    /// </summary>
    /// <returns>A string that represents the error object, including all its properties and extensions.</returns>
    public override string ToString()
    {
        var interpolatedStringHandler = new DefaultInterpolatedStringHandler();
        interpolatedStringHandler.AppendFormatted($"Code: {Code}");
        interpolatedStringHandler.AppendLiteral(", ");
        interpolatedStringHandler.AppendFormatted($"Name: {Name}");
        interpolatedStringHandler.AppendLiteral(", ");
        interpolatedStringHandler.AppendFormatted($"Description: {Description}");

        foreach (var extension in Extensions)
        {
            interpolatedStringHandler.AppendLiteral(", ");
            interpolatedStringHandler.AppendFormatted($"{extension.Key}: {extension.Value}");
        }

        return interpolatedStringHandler.ToStringAndClear();
    }
}