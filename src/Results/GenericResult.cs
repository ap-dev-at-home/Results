using System.Reflection;

namespace Results;

public class Result<TValue> : Result
{
    private TValue value;

    /// <summary>
    /// The value of the result.
    /// If the object is a handover, the value is an array of nullable objects.
    /// </summary>
    public TValue Value 
    {
        get => this.value;
        internal set 
        { 
            if (value != null && value.GetType().IsAssignableTo(typeof(Result)) == true)
            {
                throw new InvalidOperationException("Value cannot be a Result object.");
            }

            this.value = value;
        }
    }

    internal Result() 
    { 

    }

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

    /// <summary>
    /// Sets a value if the current value is null.
    /// Sets the result successful.
    /// </summary>
    /// <param name="value">The value to set.</param>
    /// <returns>The current result.</returns>
    public Result<TValue> WhenNull(TValue value)
    {
        if (value != null && value.GetType().IsAssignableTo(typeof(Result)) == true)
        {
            throw new InvalidOperationException("Value cannot be a Result object.");
        }

        if (base.Failed == true)
        {
            return this;
        }

        if (this.Value == null)
        {
            this.Value = value;
            this.Success = true;
        }

        return this;
    }

    /// <summary>
    /// Sets value from func if eval returns true. 
    /// Sets the result successful.
    /// </summary>
    /// <param name="eval">The evaluation function.</param>
    /// <param name="func">The value funtion.</param>
    /// <returns>The current result.</returns>
    public Result<TValue> When(Func<TValue, bool> eval, Func<TValue, TValue> func)
    {
        if (base.Failed == true)
        {
            return this;
        }

        var evalResult = eval(this.Value);
        TValue? value = default;
        if (evalResult == true)
        {
            value = func(this.Value);
        }

        if (value != null && value.GetType().IsAssignableTo(typeof(Result)) == true)
        {
            throw new InvalidOperationException("Value cannot be a Result object.");
        }

        if (evalResult == true)
        {
            this.Value = value;
            this.Success = true;
        }

        return this;
    }

    /// <summary>
    /// Sets the result failed if func returns false.
    /// Sets error message if func returns false.
    /// </summary>
    /// <param name="func">The assertion function.</param>
    /// <param name="message">The error message.</param>
    /// <returns>The current result.</returns>
    public Result<TValue> Assert(Func<TValue, bool> func, string message)
    {
        if (base.Failed == true)
        {
            return this;
        }

        if (func(this.Value) == false)
        {
            base.Success = false;
            base.Error = new Error(message);
        }

        return this;
    }

    /// <summary>
    /// Sets the result failed if func returns false.
    /// Sets error if func returns false.
    /// </summary>
    /// <param name="func">The assertion function.</param>
    /// <param name="error">The error.</param>
    /// <returns>The current result.</returns>
    public Result<TValue> Assert(Func<TValue, bool> func, Error error)
    {
        if (base.Failed == true)
        {
            return this;
        }

        if (func(this.Value) == false)
        {
            base.Success = false;
            base.Error = error;
        }

        return this;
    }
}