namespace Pavas.Patterns.Result.Errors;

public sealed record Error
{
    public required string Code { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public Dictionary<string, object?> Extensions { get; init; } = new(StringComparer.Ordinal);
}