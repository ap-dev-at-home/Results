namespace Results;

public partial class Result
{
    /// <summary>
    /// Executes a function without parameters.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <returns>The result return from func.</returns>
    public static Result<TResult> Do<TResult>(Func<Result<TResult>> func)
    {
        return func();
    }

    /// <summary>
    /// Executes an asynchronous function without parameters.
    /// </summary>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <returns>A task representing the result returned from func.</returns>
    public static async Task<Result<TResult>> DoAsync<TResult>(Func<Task<Result<TResult>>> func)
    {
        return await func();
    }
}
