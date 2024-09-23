using Pavas.Patterns.Result.Errors;

namespace Pavas.Patterns.Result;

/// <summary>
/// Provides extension methods for handling results and error handling in a functional style.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Binds the result of a previous operation to a new function, propagating success or failure.
    /// </summary>
    /// <typeparam name="TIn">The input type of the result.</typeparam>
    /// <typeparam name="TOut">The output type of the result after applying the bind function.</typeparam>
    /// <param name="result">The original result to bind.</param>
    /// <param name="bind">The function to apply if the result is successful.</param>
    /// <returns>A new result of type <typeparamref name="TOut"/> based on the success or failure of the original result.</returns>
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> bind)
    {
        return result.IsSuccess ? bind(result.Value!) : Result<TOut>.Failure(result.Error!);
    }

    /// <summary>
    /// Binds a result without a value to a new function, propagating success or failure.
    /// </summary>
    /// <typeparam name="TOut">The output type of the result after applying the bind function.</typeparam>
    /// <param name="result">The original result to bind.</param>
    /// <param name="bind">The function to apply if the result is successful.</param>
    /// <returns>A new result of type <typeparamref name="TOut"/> based on the success or failure of the original result.</returns>
    public static Result<TOut> Bind<TOut>(this Result result, Func<Result<TOut>> bind)
    {
        return result.IsSuccess ? bind() : Result<TOut>.Failure(result.Error!);
    }

    /// <summary>
    /// Attempts to apply a function that could throw an exception. If the function succeeds, it returns a successful result; otherwise, it returns a failure.
    /// </summary>
    /// <typeparam name="TIn">The input type of the result.</typeparam>
    /// <typeparam name="TOut">The output type of the result after applying the function.</typeparam>
    /// <param name="result">The original result to try the function on.</param>
    /// <param name="tryCase">The function to apply, which may throw an exception.</param>
    /// <param name="error">The error to return if an exception is thrown.</param>
    /// <returns>A new result of type <typeparamref name="TOut"/> based on the success or failure of the operation.</returns>
    public static Result<TOut> TryCatch<TIn, TOut>(this Result<TIn> result, Func<TIn, TOut> tryCase, Error error)
    {
        try
        {
            return result.IsSuccess
                ? Result<TOut>.Success(tryCase(result.Value!))
                : Result<TOut>.Failure(result.Error!);
        }
        catch
        {
            return Result<TOut>.Failure(error);
        }
    }

    /// <summary>
    /// Attempts to apply a function that could throw an exception. If the function succeeds, it returns a successful result; otherwise, it returns a failure.
    /// </summary>
    /// <typeparam name="TOut">The output type of the result after applying the function.</typeparam>
    /// <param name="result">The original result to try the function on.</param>
    /// <param name="tryCase">The function to apply, which may throw an exception.</param>
    /// <param name="error">The error to return if an exception is thrown.</param>
    /// <returns>A new result of type <typeparamref name="TOut"/> based on the success or failure of the operation.</returns>
    public static Result<TOut> TryCatch<TOut>(this Result result, Func<TOut> tryCase, Error error)
    {
        try
        {
            return result.IsSuccess
                ? Result<TOut>.Success(tryCase())
                : Result<TOut>.Failure(result.Error!);
        }
        catch
        {
            return Result<TOut>.Failure(error);
        }
    }

    /// <summary>
    /// Attempts to apply a function that could throw an exception, with a custom error handler for exceptions.
    /// </summary>
    /// <typeparam name="TIn">The input type of the result.</typeparam>
    /// <typeparam name="TOut">The output type of the result after applying the function.</typeparam>
    /// <param name="result">The original result to try the function on.</param>
    /// <param name="tryCase">The function to apply, which may throw an exception.</param>
    /// <param name="catchCase">The function that returns a custom <see cref="Error"/> based on the exception thrown.</param>
    /// <returns>A new result of type <typeparamref name="TOut"/> based on the success or failure of the operation.</returns>
    public static Result<TOut> TryCatch<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> tryCase,
        Func<Exception, Error> catchCase
    )
    {
        try
        {
            return result.IsSuccess
                ? Result<TOut>.Success(tryCase(result.Value!))
                : Result<TOut>.Failure(result.Error!);
        }
        catch (Exception e)
        {
            var error = catchCase(e);
            return Result<TOut>.Failure(error);
        }
    }

    /// <summary>
    /// Attempts to apply a function that could throw an exception, with a custom error handler for exceptions.
    /// </summary>
    /// <typeparam name="TOut">The output type of the result after applying the function.</typeparam>
    /// <param name="result">The original result to try the function on.</param>
    /// <param name="tryCase">The function to apply, which may throw an exception.</param>
    /// <param name="catchCase">The function that returns a custom <see cref="Error"/> based on the exception thrown.</param>
    /// <returns>A new result of type <typeparamref name="TOut"/> based on the success or failure of the operation.</returns>
    public static Result<TOut> TryCatch<TOut>(
        this Result result, Func<Result<TOut>> tryCase,
        Func<Exception, Error> catchCase
    )
    {
        try
        {
            return result.IsSuccess
                ? tryCase()
                : Result<TOut>.Failure(result.Error!);
        }
        catch (Exception e)
        {
            var error = catchCase(e);
            return Result<TOut>.Failure(error);
        }
    }

    /// <summary>
    /// Executes an action if the result is successful.
    /// </summary>
    /// <typeparam name="TIn">The type of the result value.</typeparam>
    /// <param name="result">The original result to check for success.</param>
    /// <param name="action">The action to execute if the result is successful.</param>
    /// <returns>The original result after executing the action.</returns>
    public static Result<TIn> ThenSuccess<TIn>(this Result<TIn> result, Action<TIn> action)
    {
        if (result.IsSuccess)
        {
            action(result.Value!);
        }

        return result;
    }

    /// <summary>
    /// Executes an action if the result is successful (no result value).
    /// </summary>
    /// <param name="result">The original result to check for success.</param>
    /// <param name="action">The action to execute if the result is successful.</param>
    /// <returns>The original result after executing the action.</returns>
    public static Result ThenSuccess(this Result result, Action action)
    {
        if (result.IsSuccess)
        {
            action();
        }

        return result;
    }

    /// <summary>
    /// Executes an action if the result is a failure.
    /// </summary>
    /// <typeparam name="TIn">The type of the result value.</typeparam>
    /// <param name="result">The original result to check for failure.</param>
    /// <param name="action">The action to execute if the result is a failure.</param>
    /// <returns>The original result after executing the action.</returns>
    public static Result<TIn> ThenFailure<TIn>(this Result<TIn> result, Action<Error> action)
    {
        if (result.IsFailure)
        {
            action(result.Error!);
        }

        return result;
    }

    /// <summary>
    /// Matches the result based on its success or failure, returning a value from the corresponding function.
    /// </summary>
    /// <typeparam name="T">The return type of the match functions.</typeparam>
    /// <param name="result">The original result to match.</param>
    /// <param name="onSuccess">The function to call if the result is successful.</param>
    /// <param name="onFailure">The function to call if the result is a failure.</param>
    /// <returns>The result of the function corresponding to the result state.</returns>
    public static T Match<T>(this Result result, Func<T> onSuccess, Func<Error, T> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Error!);
    }

    /// <summary>
    /// Matches the result based on its success or failure, returning a value from the corresponding function.
    /// </summary>
    /// <typeparam name="TValue">The type of the result value.</typeparam>
    /// <typeparam name="T">The return type of the match functions.</typeparam>
    /// <param name="result">The original result to match.</param>
    /// <param name="onSuccess">The function to call if the result is successful.</param>
    /// <param name="onFailure">The function to call if the result is a failure.</param>
    /// <returns>The result of the function corresponding to the result state.</returns>
    public static T Match<TValue, T>(this Result<TValue> result, Func<TValue, T> onSuccess, Func<Error, T> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value!) : onFailure(result.Error!);
    }
}