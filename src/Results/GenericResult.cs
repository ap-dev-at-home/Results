﻿namespace Results;

public partial class Result<TValue> : Result
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
    /// When failed, creates a successful result with a value.
    /// </summary>
    /// <returns>A new result instance</returns>
    public Result<TValue> Recover(TValue value)
    {
        if (base.Failed == true)
        {
            return Result.Ok(value);
        }
        
        return this;
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
    public Result<TValue> Assert(Func<TValue, bool> func, string? message)
    {
        return this.Assert(func, string.IsNullOrEmpty(message) == false ? new Error(message) : null);
    }

    /// <summary>
    /// Sets the result failed if func returns false.
    /// Sets error if func returns false.
    /// </summary>
    /// <param name="func">The assertion function.</param>
    /// <param name="error">The error.</param>
    /// <returns>The current result.</returns>
    public Result<TValue> Assert(Func<TValue, bool> func, Error? error)
    {
        if (base.Failed == true)
        {
            return this;
        }

        if (func(this.Value) == false)
        {
            base.Success = false;
            if (error != null)
            {
                base.Error = error;
            }
        }

        return this;
    }

    /// <summary>
    /// Implicitly converts the Result object to its underlying value type.
    /// </summary>
    /// <param name="result">The Result object.</param>
    public static implicit operator TValue(Result<TValue> result)
    {
        return result.Value;
    }
}