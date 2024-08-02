using Pavas.Patterns.Result.Errors;

namespace Pavas.Patterns.Result.Contracts;

public interface IResult
{
    public Error? Error { get; init; }
    public bool IsSuccess { get; init; }
    public bool IsFailure { get; init; }
}

public interface IResult<TValue>
{
    public TValue? Value { get; init; }
    public Error? Error { get; init; }
    public bool IsSuccess { get; init; }
    public bool IsFailure { get; init; }
}