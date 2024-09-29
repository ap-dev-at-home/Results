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
    /// Executes a function with 1 parameters.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <returns>The result return from func.</returns>
    public static Result<TResult> Do<T1, TResult>(Func<T1, Result<TResult>> func, T1 arg1)
    {
        return func(arg1);
    }

    /// <summary>
    /// Executes a function with 2 parameters.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <returns>The result return from func.</returns>
    public static Result<TResult> Do<T1, T2, TResult>(Func<T1, T2, Result<TResult>> func, T1 arg1, T2 arg2)
    {
        return func(arg1, arg2);
    }

    /// <summary>
    /// Executes a function with 3 parameters.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <returns>The result return from func.</returns>
    public static Result<TResult> Do<T1, T2, T3, TResult>(Func<T1, T2, T3, Result<TResult>> func, T1 arg1, T2 arg2, T3 arg3)
    {
        return func(arg1, arg2, arg3);
    }

    /// <summary>
    /// Executes a function with 4 parameters.
    /// </summary>
    /// <param name="func">The function to execute.</param>
    /// <returns>The result return from func.</returns>
    public static Result<TResult> Do<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, Result<TResult>> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        return func(arg1, arg2, arg3, arg4);
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

    /// <summary>
    /// Executes an asynchronous function with 1 parameter.
    /// </summary>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <returns>A task representing the result returned from func.</returns>
    public static async Task<Result<TResult>> DoAsync<T1, TResult>(Func<T1, Task<Result<TResult>>> func, T1 arg1)
    {
        return await func(arg1);
    }

    /// <summary>
    /// Executes an asynchronous function with 2 parameters.
    /// </summary>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <returns>A task representing the result returned from func.</returns>
    public static async Task<Result<TResult>> DoAsync<T1, T2, TResult>(Func<T1, T2, Task<Result<TResult>>> func, T1 arg1, T2 arg2)
    {
        return await func(arg1, arg2);
    }

    /// <summary>
    /// Executes an asynchronous function with 3 parameters.
    /// </summary>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <returns>A task representing the result returned from func.</returns>
    public static async Task<Result<TResult>> DoAsync<T1, T2, T3, TResult>(Func<T1, T2, T3, Task<Result<TResult>>> func, T1 arg1, T2 arg2, T3 arg3)
    {
        return await func(arg1, arg2, arg3);
    }

    /// <summary>
    /// Executes an asynchronous function with 4 parameters.
    /// </summary>
    /// <param name="func">The asynchronous function to execute.</param>
    /// <returns>A task representing the result returned from func.</returns>
    public static async Task<Result<TResult>> DoAsync<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, Task<Result<TResult>>> func, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        return await func(arg1, arg2, arg3, arg4);
    }
}
