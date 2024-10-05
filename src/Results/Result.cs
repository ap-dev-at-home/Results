namespace Results;

public partial class Result
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
        => new ResultCollection() { Success = true, Value = values };

    /// <summary>
    /// Calls the given functions until the first failed result is returned.
    /// If a function fails, a failed ResultCollection is returned immediately.
    /// If no function fails, a successful ResultCollection is returned.
    /// </summary>
    /// <param name="funcs">The functions to call.</param>
    /// <returns>A ResultCollection reflecting all results.</returns>
    public static ResultCollection FailFast(params Func<Result>[] funcs)
    {
        List<Result> results = [];
        bool success = true;

        foreach (var func in funcs)
        {
            var  result = func();
            results.Add(result);
            if (result.Success == false)
            {
                success = false;
                break;
            }
        }

        return new ResultCollection
        {
            Success = success,
            Value = [.. results]
        };
    }

    /// <summary>
    /// Calls all given functions.
    /// </summary>
    /// <param name="funcs">The functions to call.</param>
    /// <returns>A ResultCollection reflecting all results.</returns>
    public static ResultCollection FailSafe(params Func<Result>[] funcs)
    {
        var results = new Result[funcs.Length];
        bool success = true;
        
        for (var i = 0; i < funcs.Length; i++)
        {
            results[i] = funcs[i]();
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

    public Result WithLogs(Result result)
    {
        this.LogEntries.InsertRange(0, result.LogEntries);

        return this;
    }

    public Result<T> Cast<T>()
    {
        return (Result<T>)this;
    }
}