namespace Results;

public class Result
{
    protected List<LogEntry> LogEntries { get; set; } = [];

    /// <summary>
    /// The log entries created on this result.
    /// </summary>
    public IReadOnlyCollection<LogEntry> Logs => this.LogEntries.AsReadOnly();

    /// <summary>
    /// The error this result failed with.
    /// </summary>
    public Error? Error { get; internal set; }

    /// <summary>
    /// Returns true if the result is in a success status.
    /// </summary>
    public bool Success { get; internal set; }

    /// <summary>
    /// Returns true if the result is in a failed status.
    /// </summary>
    public bool Failed { get => !this.Success; }

    internal Result()
    { 
    
    }

    public Result LogIf(bool condition, string message, LogEntryType? logEntryType = null)
    {
        if (condition == true)
        {
            this.LogEntries.Add(new LogEntry(message, logEntryType ?? LogEntryType.Info));
        }

        return this;
    }

    public Result Log(string message, LogEntryType? logEntryType = null)
    {
        this.LogEntries.Add(new LogEntry(message, logEntryType ?? LogEntryType.Info));

        return this;
    }

    public Result Log(Exception exception, string? message = null)
    {
        this.LogEntries.Add(new LogEntry(exception, message));

        return this;
    }

    public Result WithLogs(List<LogEntry> logs)
    {
        this.LogEntries.InsertRange(0, logs);

        return this;
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
    /// Creates a failed result containing the error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <returns>The result object.</returns>
    public static Result Fail(string? message)
        => new() 
        { 
            Success = false,
            Error = (message == null) ? null : new Error(message)
        };

    /// <summary>
    /// Creates a failed result containing error.
    /// </summary>
    /// <param name="error">The Error.</param>
    /// <returns>The result object.</returns>
    public static Result Fail(Error? error)
        => new()
        {
            Success = false,
            Error = error
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
    /// Creates a typed failed result containing error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <returns>The result object.</returns>
    public static Result<T> Fail<T>(string? message = null)
        => new() 
        { 
            Success = false,
            Error = (message == null) ? null : new Error(message)
        };

    /// <summary>
    /// Creates a typed failed result containing error.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <returns>The result object.</returns>
    public static Result<T> Fail<T>(Error? error = null)
        => new()
        {
            Success = false,
            Error = error
        };

    /// <summary>
    /// Creates a typed result containing a value.
    /// The Result is successful if the value is not null.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="messages">Error message.</param>
    /// <returns>The result object.</returns>
    public static Result<T> NotNull<T>(T? value, string? message = null)
        => new() 
        { 
            Success = value != null,
            Value = value,
            Error = (message == null) ? null : new Error(message)
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
    //public static Result FailSafe(params Func<Result>[] funcs)
    //{
    //    List<Result> results = [];

    //    foreach (var func in funcs)
    //    {
    //        var result = func();
    //        if (result.Failed)
    //        {
    //            results.Add(result);
    //        }
    //    }

    //    if (results.Count > 0)
    //    {
    //        return Result.Fail(results.SelectMany(r => r.Errors).ToArray());
    //    }

    //    return Result.Ok();
    //}
}