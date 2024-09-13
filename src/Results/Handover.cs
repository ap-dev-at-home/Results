using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Results.Test")]

namespace Results;

internal class Handover : Result<object?[]>
{
    internal Handover()
        : base()
    { 
    
    }

    private object?[] GetArray()
    {
        var array = (base.Value as Array) ?? throw new InvalidOperationException("Value is not an array.");
        return array.Cast<object>().ToArray();
    }

    protected override Result<TResult> Call<TResult>(MethodInfo? method, object? target)
    {
        ArgumentNullException.ThrowIfNull(method, nameof(method));
        ArgumentNullException.ThrowIfNull(target, nameof(target));

        if (base.Success == false)
        {
            return new() { Success = false, Errors = base.Errors };
        }

        bool hasFailed = false;
        List<Error> errors = [];

        var parameter = this.GetArray().Select(v =>
        {
            if (v == null)
            {
                return v;
            }

            var type = v.GetType();
            var isResult = type.IsAssignableTo(typeof(Result));

            if (isResult == true)
            {
                var successProperty = type.GetProperty("Success");
                if (successProperty?.GetValue(v) is bool success && success == false)
                {
                    hasFailed = true;
                    errors.AddRange(((Result)v).Errors);
                }

                var valueProperty = type.GetProperty("Value");
                return valueProperty?.GetValue(v);
            }
            else
            {
                return v;
            }
        }).ToArray();

        if (hasFailed == true)
        {
            return new() { Success = false, Errors = [.. base.Errors, .. errors] };
        }

        var result = method.Invoke(target, parameter);

        var castResult = (result as Result<TResult>) ?? throw new InvalidOperationException("Method does not return a Result<TResult>.");

        if (castResult.Success == false)
        {
            castResult.Errors.InsertRange(0, base.Errors);
        }

        return castResult;
    }
}