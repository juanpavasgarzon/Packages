using System.Runtime.CompilerServices;

namespace Pavas.Patterns.Result.Errors;

public sealed record Error
{
    public required string Code { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public Dictionary<string, object?> Extensions { get; init; } = new(StringComparer.Ordinal);

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