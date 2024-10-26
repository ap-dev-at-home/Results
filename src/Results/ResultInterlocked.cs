namespace Results;

public partial class Result
{
    /// <summary>
    /// Executes a function within an interlocked section, ensuring exclusive access to the provided lock object.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="func">The function to execute.</param>
    /// <param name="l">The lock object.</param>
    /// <param name="wait">A flag indicating whether to wait for the lock or not.</param>
    /// <returns>The result of the function execution.</returns>
    public static Result<TResult> DoInterlocked<TResult>(Func<Result<TResult>> func, object l, bool wait = true)
    {
        if (wait == true)
        {
            Monitor.Enter(l);
        }
        else if (Monitor.TryEnter(l) == false)
        {
            return Result.Fail<TResult>(new InterlockError());
        }

        try
        {
            return func();
        }
        finally
        {
            Monitor.Exit(l);
        }
    }

    /// <summary>
    /// Executes a function within an interlocked section, ensuring exclusive access to the provided lock object.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <param name="l">The lock object.</param>
    /// <param name="wait">A flag indicating whether to wait for the lock or not.</param>
    /// <returns>The result of the function execution.</returns>
    public static Result DoInterlocked(Func<Result> func, object l, bool wait = true)
    {
        if (wait == true)
        {
            Monitor.Enter(l);
        }
        else if (Monitor.TryEnter(l) == false)
        {
            return Result.Fail(new InterlockError());
        }

        try
        {
            return func();
        }
        finally
        {
            Monitor.Exit(l);
        }
    }

    /// <summary>
    /// Executes a function within an interlocked section, ensuring exclusive access to the provided lock object.
    /// If an exception occurs during the function execution, it is caught and a failed result with the exception is returned.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <param name="l">The lock object.</param>
    /// <param name="wait">A flag indicating whether to wait for the lock or not.</param>
    /// <param name="catch">An optional action to handle the caught exception.</param>
    /// <returns>The result of the function execution.</returns>
    public static Result TryInterlocked(Func<Result> func, object l, bool wait = true, Action<Exception>? @catch = null)
    {
        if (wait == true)
        {
            Monitor.Enter(l);
        }
        else if (Monitor.TryEnter(l) == false)
        {
            return Result.Fail(new InterlockError());
        }

        try
        {
            return func();
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail(new ExceptionError(ex));
        }
        finally
        {
            Monitor.Exit(l);
        }
    }

    /// <summary>
    /// Executes a function within an interlocked section, ensuring exclusive access to the provided lock object.
    /// If an exception occurs during the function execution, it is caught and a failed result with the exception is returned.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="func">The function to execute.</param>
    /// <param name="l">The lock object.</param>
    /// <param name="wait">A flag indicating whether to wait for the lock or not.</param>
    /// <param name="catch">An optional action to handle the caught exception.</param>
    /// <returns>The result of the function execution.</returns>
    public static Result<TResult> TryInterlocked<TResult>(Func<Result<TResult>> func, object l, bool wait = true, Action<Exception>? @catch = null)
    {
        if (wait == true)
        {
            Monitor.Enter(l);
        }
        else if (Monitor.TryEnter(l) == false)
        {
            return Result.Fail<TResult>(new InterlockError());
        }

        try
        {
            return func();
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail<TResult>(new ExceptionError(ex));
        }
        finally
        {
            Monitor.Exit(l);
        }
    }
}
