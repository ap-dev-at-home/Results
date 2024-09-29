namespace Results;

public partial class Result
{
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

    /// <summary>
    /// Executes a function with no parameters and catches any exceptions.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs otherwise successful result with value.</returns>
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

    /// <summary>
    /// Executes a function with 1 parameters and catches any exceptions.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs otherwise successful result with value.</returns>
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

    /// <summary>
    /// Executes a function with 2 parameters and catches any exceptions.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs otherwise successful result with value.</returns>
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

    /// <summary>
    /// Executes a function with 3 parameters and catches any exceptions.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs otherwise successful result with value.</returns>
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

    /// <summary>
    /// Executes a function with 4 parameters and catches any exceptions.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs otherwise successful result with value.</returns>
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
    /// Asynchronously executes a function with no parameters and catches any exceptions.
    /// </summary>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs, otherwise a successful result with value.</returns>
    public static async Task<Result<TResult>> TryAsync<TResult>(Func<Task<Result<TResult>>> func, Action<Exception>? @catch = null)
    {
        try
        {
            return await func();
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail<TResult>(new ExceptionError(ex));
        }
    }

    /// <summary>
    /// Asynchronously executes a function with 1 parameter and catches any exceptions.
    /// </summary>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs, otherwise a successful result with value.</returns>
    public static async Task<Result<TResult>> TryAsync<T1, TResult>(Func<T1, Task<Result<TResult>>> func, T1 arg1, Action<Exception>? @catch = null)
    {
        try
        {
            return await func(arg1);
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail<TResult>(new ExceptionError(ex));
        }
    }

    /// <summary>
    /// Asynchronously executes a function with 2 parameters and catches any exceptions.
    /// </summary>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs, otherwise a successful result with value.</returns>
    public static async Task<Result<TResult>> TryAsync<T1, T2, TResult>(Func<T1, T2, Task<Result<TResult>>> func, T1 arg1, T2 arg2, Action<Exception>? @catch = null)
    {
        try
        {
            return await func(arg1, arg2);
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail<TResult>(new ExceptionError(ex));
        }
    }

    /// <summary>
    /// Asynchronously executes a function with 3 parameters and catches any exceptions.
    /// </summary>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs, otherwise a successful result with value.</returns>
    public static async Task<Result<TResult>> TryAsync<T1, T2, T3, TResult>(Func<T1, T2, T3, Task<Result<TResult>>> func, T1 arg1, T2 arg2, T3 arg3, Action<Exception>? @catch = null)
    {
        try
        {
            return await func(arg1, arg2, arg3);
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail<TResult>(new ExceptionError(ex));
        }
    }

    /// <summary>
    /// Asynchronously executes a function with 4 parameters and catches any exceptions.
    /// </summary>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs, otherwise a successful result with value.</returns>
    public static async Task<Result<TResult>> TryAsync<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, Task<Result<TResult>>> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4, Action<Exception>? @catch = null)
    {
        try
        {
            return await func(arg1, arg2, arg3, arg4);
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail<TResult>(new ExceptionError(ex));
        }
    }
}