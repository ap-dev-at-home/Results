using System.Text.Json;

namespace Results.Json;

/// <summary>
/// Provides methods for deserializing JSON strings or files into Result objects.
/// </summary>
/// <typeparam name="T">The type of the object to deserialize.</typeparam>
public static class Json
{
    /// <summary>
    /// Deserializes a JSON string into a Result object.
    /// </summary>
    /// <param name="jsonString">The JSON string to deserialize.</param>
    /// <returns>A Result object containing the deserialized object.</returns>
    public static Result<T> From<T>(string jsonString)
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

    /// <summary>
    /// Deserializes a JSON file into a Result object.
    /// </summary>
    /// <param name="path">The path to the JSON file.</param>
    /// <returns>A Result object containing the deserialized object.</returns>
    public static Result<T> Load<T>(string path)
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

    /// <summary>
    /// Serializes an object to a JSON file.
    /// </summary>
    /// <param name="path">The path to the JSON file.</param>
    /// <param name="obj">The object to serialize.</param>
    /// <returns></returns>
    public static Result Save<T>(string path, T obj)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            File.WriteAllText(path, JsonSerializer.Serialize(obj, options));

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(new ExceptionError(ex));
        }
    }

    /// <summary>
    /// Deserializes a JSON stream into a Result object.
    /// </summary>
    /// <param name="stream">The stream containing the JSON data.</param>
    /// <returns>A Result object containing the deserialized object.</returns>
    public static Result<T> From<T>(Stream stream)
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

    /// <summary>
    /// Deserializes a JSON byte span into a Result object.
    /// </summary>
    /// <param name="utf8Json">The byte span containing the JSON data.</param>
    /// <returns>A Result object containing the deserialized object.</returns>
    public static Result<T> From<T>(ReadOnlySpan<byte> utf8Json)
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