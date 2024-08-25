using Pavas.Patterns.Result.Errors;

namespace Pavas.Patterns.Result;

public static class ResultExtensions
{
    public static Result<TOut> Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Result<TOut>> bind)
    {
        return result.IsSuccess ? bind(result.Value!) : Result<TOut>.Failure(result.Error!);
    }

    public static Result<TOut> Bind<TOut>(this Result result, Func<Result<TOut>> bind)
    {
        return result.IsSuccess ? bind() : Result<TOut>.Failure(result.Error!);
    }

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

    public static Result<TIn> ThenSuccess<TIn>(this Result<TIn> result, Action<TIn> action)
    {
        if (result.IsSuccess)
        {
            action(result.Value!);
        }

        return result;
    }

    public static Result ThenSuccess(this Result result, Action action)
    {
        if (result.IsSuccess)
        {
            action();
        }

        return result;
    }

    public static Result<TIn> ThenFailure<TIn>(this Result<TIn> result, Action<Error> action)
    {
        if (result.IsFailure)
        {
            action(result.Error!);
        }

        return result;
    }

    public static T Match<T>(this Result result, Func<T> onSuccess, Func<Error, T> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result.Error!);
    }

    public static T Match<TValue, T>(this Result<TValue> result, Func<TValue, T> onSuccess, Func<Error, T> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value!) : onFailure(result.Error!);
    }
}