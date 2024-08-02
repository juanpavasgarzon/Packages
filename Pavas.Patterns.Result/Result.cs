using Pavas.Patterns.Result.Contracts;
using Pavas.Patterns.Result.Errors;

namespace Pavas.Patterns.Result;

public sealed class Result : IResult
{
    public Error? Error { get; init; }
    public required bool IsSuccess { get; init; }
    public required bool IsFailure { get; init; }

    public static Result Success() => new()
    {
        IsSuccess = true,
        IsFailure = false,
        Error = default
    };

    public static Result Failure(Error error) => new()
    {
        IsSuccess = false,
        IsFailure = true,
        Error = error
    };
}

public sealed class Result<TValue> : IResult<TValue>
{
    public TValue? Value { get; init; }
    public Error? Error { get; init; }
    public required bool IsSuccess { get; init; }
    public required bool IsFailure { get; init; }

    public static Result<TValue> Success(TValue value) => new()
    {
        Value = value,
        IsSuccess = true,
        IsFailure = false,
        Error = default
    };

    public static Result<TValue> Failure(Error error) => new()
    {
        Value = default,
        IsSuccess = false,
        IsFailure = true,
        Error = error
    };
}