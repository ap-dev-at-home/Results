using System.Reflection;

namespace Results;

public partial class Result<TValue> : Result
{
    /// <summary>
    /// Calls func if the result returned by the preceeding call was successful.
    /// </summary>
    /// <returns>The result returned from func.</returns>
    public Result<TResult> Then<TResult>(Func<TValue, Result<TResult>> func)
    {
        return this.Call<TResult>(func.Method, func.Target);
    }

    /// <summary>
    /// Calls func if the result returned by the preceeding call was successful.
    /// </summary>
    /// <returns>The result returned from func.</returns>
    public Result<TResult> Then<T1, T2, TResult>(Func<T1, T2, Result<TResult>> func)
    {
        return this.Call<TResult>(func.Method, func.Target);
    }

    /// <summary>
    /// Calls func if the result returned by the preceeding call was successful.
    /// </summary>
    /// <returns>The result returned from func.</returns>
    public Result<TResult> Then<T1, T2, T3, TResult>(Func<T1, T2, T3, Result<TResult>> func)
    {
        return this.Call<TResult>(func.Method, func.Target);
    }

    /// <summary>
    /// Calls func if the result returned by the preceeding call was successful.
    /// </summary>
    /// <returns>The result returned from func.</returns>
    public Result<TResult> Then<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, Result<TResult>> func)
    {
        return this.Call<TResult>(func.Method, func.Target);
    }

    protected virtual Result<TResult> Call<TResult>(MethodInfo? method, object? target)
    {
        ArgumentNullException.ThrowIfNull(method, nameof(method));
        ArgumentNullException.ThrowIfNull(target, nameof(target));

        if (base.Success == false)
        {
            return new() { Success = false, Error = base.Error };
        }

        var result = method.Invoke(target, [this.Value]);

        var castResult = (result as Result<TResult>)
            ?? throw new InvalidOperationException("Method does not return a Result<TResult>.");

        return castResult;
    }
}
