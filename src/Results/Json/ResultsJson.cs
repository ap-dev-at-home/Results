using System.Text.Json;

namespace Results.Json;

public static class ResultsJson<T>
{
    public static Result<T> From(string jsonString)
    {
        try
        {
            T? obj = JsonSerializer.Deserialize<T>(jsonString);

            return Result.Ok(obj);
        }
        catch (Exception ex)
        {
            return Result.Fail<T>(new ExceptionError(ex));
        }
    }

    public static Result<T> Load(string path)
    {
        try
        {
            if (File.Exists(path) == false)
            {
                return Result.Fail<T>(new ExceptionError(new FileNotFoundException(path)));
            }

            T? obj = JsonSerializer.Deserialize<T>(File.ReadAllText(path));

            return Result.Ok(obj);
        }
        catch (Exception ex)
        {
            return Result.Fail<T>(new ExceptionError(ex));
        }
    }

    public static Result<T> From(Stream stream)
    {
        try
        {
            T? obj = JsonSerializer.Deserialize<T>(stream);

            return Result.Ok(obj);
        }
        catch (Exception ex)
        {
            return Result.Fail<T>(new ExceptionError(ex));
        }
    }

    public static Result<T> From(ReadOnlySpan<byte> utf8Json)
    {
        try
        {
            T? obj = JsonSerializer.Deserialize<T>(utf8Json);

            return Result.Ok(obj);
        }
        catch (Exception ex)
        {
            return Result.Fail<T>(new ExceptionError(ex));
        }
    }
}