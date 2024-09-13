namespace Results;

public class Result
{
    /// <summary>
    /// The list of errors that occurred during the call chain.
    /// </summary>
    public List<Error> Errors { get; init; } = [];

    /// <summary>
    /// Returns true if the call chain did not fail.
    /// </summary>
    public bool Success { get; internal set; }

    /// <summary>
    /// Returns true if the call chain failed.
    /// </summary>
    public bool Failed { get => !this.Success; }

    internal Result()
    { 
    
    }

    /// <summary>
    /// Starts a call or call chain. With return value but without parameters.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <returns>The result return from func.</returns>
    public static Result<TResult> Do<TResult>(Func<Result<TResult>> func)
    {
        return func();
    }

    /// <summary>
    /// Starts a call or call chain. With return value and 1 parameters.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <returns>The result return from func.</returns>
    public static Result<TResult> Do<T1, TResult>(Func<T1, Result<TResult>> func, T1 arg1)
    {
        return func(arg1);
    }

    /// <summary>
    /// Starts a call or call chain. With return value and 2 parameters.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <returns>The result return from func.</returns>
    public static Result<TResult> Do<T1, T2, TResult>(Func<T1, T2, Result<TResult>> func, T1 arg1, T2 arg2)
    {
        return func(arg1, arg2);
    }

    /// <summary>
    /// Starts a call or call chain. With return value and 3 parameters.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <returns>The result return from func.</returns>
    public static Result<TResult> Do<T1, T2, T3, TResult>(Func<T1, T2, T3, Result<TResult>> func, T1 arg1, T2 arg2, T3 arg3)
    {
        return func(arg1, arg2, arg3);
    }

    /// <summary>
    /// Starts a call or call chain. With return value and 4 parameters.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <returns>The result return from func.</returns>
    public static Result<TResult> Do<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, Result<TResult>> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        return func(arg1, arg2, arg3, arg4);
    }

    /// <summary>
    /// Executes an action without parameter and catches any exceptions.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs otherwise successful result.</returns>
    public static Result Try(Action action, Action<Exception>? @catch = null)
    {
        try
        {
            action();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail(new ExceptionError(ex));
        }
    }

    /// <summary>
    /// Executes an action with 1 parameters and catches any exceptions.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs otherwise successful result.</returns>
    public static Result Try<T1>(Action<T1> action, T1 arg1, Action<Exception>? @catch = null)
    {
        try
        {
            action(arg1);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail(new ExceptionError(ex));
        }
    }

    /// <summary>
    /// Executes an action with 2 parameters and catches any exceptions.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs otherwise successful result.</returns>
    public static Result Try<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2, Action<Exception>? @catch = null)
    {
        try
        {
            action(arg1, arg2);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail(new ExceptionError(ex));
        }
    }

    /// <summary>
    /// Executes an action with 3 parameters and catches any exceptions.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs otherwise successful result.</returns>
    public static Result Try<T1, T2, T3>(Action<T1, T2, T3> action, T1 arg1, T2 arg2, T3 arg3, Action<Exception>? @catch = null)
    {
        try
        {
            action(arg1, arg2, arg3);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail(new ExceptionError(ex));
        }
    }

    /// <summary>
    /// Executes an action with 4 parameters and catches any exceptions.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs otherwise successful result.</returns>
    public static Result Try<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<Exception>? @catch = null)
    {
        try
        {
            action(arg1, arg2, arg3, arg4);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail(new ExceptionError(ex));
        }
    }

    public static Result<TResult> Try<TResult>(Func<Result<TResult>> func, Action<Exception>? @catch = null)
    {
        try
        {
            return func();
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail<TResult>(new ExceptionError(ex));
        }
    }

    public static Result<TResult> Try<T1, TResult>(Func<T1, Result<TResult>> func, T1 arg1, Action<Exception>? @catch = null)
    {
        try
        {
            return func(arg1);
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail<TResult>(new ExceptionError(ex));
        }
    }

    public static Result<TResult> Try<T1, T2, TResult>(Func<T1, T2, Result<TResult>> func, T1 arg1, T2 arg2, Action<Exception>? @catch = null)
    {
        try
        {
            return func(arg1, arg2);
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail<TResult>(new ExceptionError(ex));
        }
    }

    public static Result<TResult> Try<T1, T2, T3, TResult>(Func<T1, T2, T3, Result<TResult>> func, T1 arg1, T2 arg2, T3 arg3, Action<Exception>? @catch = null)
    {
        try
        {
            return func(arg1, arg2, arg3);
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail<TResult>(new ExceptionError(ex));
        }
    }

    public static Result<TResult> Try<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, Result<TResult>> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<Exception>? @catch = null)
    {
        try
        {
            return func(arg1, arg2, arg3, arg4);
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail<TResult>(new ExceptionError(ex));
        }
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>The result object.</returns>
    public static Result Ok()
        => new() { Success = true };

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    /// <returns>The result object.</returns>
    public static Result Fail()
        => new() { Success = false };

    /// <summary>
    /// Creates a failed result containing error messages.
    /// </summary>
    /// <param name="messages">The error messages.</param>
    /// <returns>The result object.</returns>
    public static Result Fail(params string[] messages)
        => new() 
        { 
            Success = false,
            Errors = messages.Select(m => new Error(m)).ToList()
        };

    /// <summary>
    /// Creates a failed result containing errors.
    /// </summary>
    /// <param name="errors">The Errors.</param>
    /// <returns>The result object.</returns>
    public static Result Fail(params Error[] errors)
        => new()
        {
            Success = false,
            Errors = [.. errors]
        };

    /// <summary>
    /// Creates an empty typed successful result.
    /// </summary>
    /// <returns>The result object.</returns>
    public static Result<T> Ok<T>()
        => new() { Success = true };

    /// <summary>
    /// Creates a successful typed result with a value.
    /// </summary>
    /// <param name="value">The value of the result.</param>
    /// <returns>The result object.</returns>
    public static Result<T> Ok<T>(T value)
        => new() { Success = true, Value = value };

    /// <summary>
    /// Creates a typed failed result.
    /// </summary>
    /// <returns>The result object.</returns>
    public static Result<T> Fail<T>()
        => new() { Success = false };

    /// <summary>
    /// Creates a typed failed result containing error messages.
    /// </summary>
    /// <param name="messages">The error messages.</param>
    /// <returns>The result object.</returns>
    public static Result<T> Fail<T>(params string[] messages)
        => new() 
        { 
            Success = false,
            Errors = messages.Select(m => new Error(m)).ToList<Error>()
        };

    /// <summary>
    /// Creates a typed failed result containing errors.
    /// </summary>
    /// <param name="errors">The Errors.</param>
    /// <returns>The result object.</returns>
    public static Result<T> Fail<T>(params Error[] errors)
        => new()
        {
            Success = false,
            Errors = [.. errors]
        };

    /// <summary>
    /// Creates a typed result containing a value.
    /// The Result is successful if the value is not null.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="messages">Error messages.</param>
    /// <returns>The result object.</returns>
    public static Result<T> NotNull<T>(T? value, params string[] messages)
        => new() 
        { 
            Success = value != null,
            Value = value,
            Errors = value == null ? messages.Select(m => new Error(m)).ToList<Error>() : []
        };

    /// <summary>
    /// Creates a result of type Handover, containing an array of values.
    /// A Handover object is used to pass values to a followup Then-Call.
    /// The values can be of type Result or any other type.
    /// Any value that is a Result object will be unwrapped and its value passed to the followup Then-Call.
    /// If any of the Result objects are failed, the followup Then-Call will not be executed.
    /// </summary>
    /// <param name="values">Array of values.</param>
    /// <returns>The result object.</returns>
    public static Result<object?[]> Handover(params object?[] values)
        => new Handover() { Success = true, Value = values };

    /// <summary>
    /// Calls the given functions until the first failed result is returned.
    /// If a function fails, the failed result is returned immediately.
    /// If no function fails, a successful result is returned.
    /// </summary>
    /// <param name="funcs">The functions to call.</param>
    /// <returns>The result.</returns>
    public static Result FailFast(params Func<Result>[] funcs)
    {
        foreach (var func in funcs)
        {
            var result = func();
            if (result.Failed)
            {
                return result;
            }
        }
        
        return Result.Ok();
    }

    /// <summary>
    /// Calls all given functions.
    /// If any function fails, a failed result containing all errors is returned.
    /// If no function fails, a successful result is returned.
    /// </summary>
    /// <param name="funcs">The functions to call.</param>
    /// <returns>The result.</returns>
    public static Result FailSafe(params Func<Result>[] funcs)
    {
        List<Result> results = [];

        foreach (var func in funcs)
        {
            var result = func();
            if (result.Failed)
            {
                results.Add(result);
            }
        }

        if (results.Count > 0)
        {
            return Result.Fail(results.SelectMany(r => r.Errors).ToArray());
        }

        return Result.Ok();
    }
}