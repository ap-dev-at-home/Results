namespace Results;

public partial class Result
{
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
}