using Pavas.Patterns.Result.Errors;

namespace Pavas.Patterns.Result.Contracts;

/// <summary>
/// Represents the result of an operation, with information about success or failure.
/// </summary>
public interface IResult
{
    /// <summary>
    /// Gets the error that occurred during the operation, if any.
    /// </summary>
    /// <value>An <see cref="Error"/> instance if the operation failed, or null if successful.</value>
    public Error? Error { get; init; }

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    /// <value>A boolean value that is true if the operation succeeded; otherwise, false.</value>
    public bool IsSuccess { get; init; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    /// <value>A boolean value that is true if the operation failed; otherwise, false.</value>
    public bool IsFailure { get; init; }
}

/// <summary>
/// Represents the result of an operation that returns a value, with information about success or failure.
/// </summary>
/// <typeparam name="TValue">The type of the value returned if the operation is successful.</typeparam>
public interface IResult<TValue> : IResult
{
    /// <summary>
    /// Gets the value returned by the operation, if successful.
    /// </summary>
    /// <value>An instance of <typeparamref name="TValue"/> if the operation succeeded, or null if it failed.</value>
    public TValue? Value { get; init; }
}