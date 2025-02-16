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
    /// Executes a function with no parameters and catches any exceptions.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs otherwise the functions result.</returns>
    public static Result Try(Func<Result> func, Action<Exception>? @catch = null)
    {
        try
        {
            return func();
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
    /// <returns>Failed result if exception occurs otherwise the functions result with value.</returns>
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
    /// Asynchronously executes a function with no parameters and catches any exceptions.
    /// </summary>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs otherwise the functions result with value.</returns>
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
    /// Asynchronously executes a function with no parameters and catches any exceptions.
    /// </summary>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <param name="catch">Action to call on exception.</param>
    /// <returns>Failed result if exception occurs otherwise the functions result with value.</returns>
    public static async Task<Result> TryAsync(Func<Task> func, Action<Exception>? @catch = null)
    {
        try
        {
            await func();
            return Result.Ok();
        }
        catch (Exception ex)
        {
            @catch?.Invoke(ex);
            return Result.Fail(new ExceptionError(ex));
        }
    }

    /// <summary>
    /// Calls all given actions surrounded by a try-catch block.
    /// </summary>
    /// <param name="actions">The actions to call.</param>
    /// <returns>A ResultCollection reflecting all results.</returns>
    public static ResultCollection TryFailSafe(params Action[] actions)
    {
        var results = new Result[actions.Length];
        bool success = true;

        for (var i = 0; i < actions.Length; i++)
        {
            results[i] = Result.Try(() => actions[i]());
            if (results[i].Success == false)
            {
                success = false;
            }
        }

        return new ResultCollection
        {
            Success = success,
            Value = results
        };
    }
}