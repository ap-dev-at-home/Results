using System.Text.Json.Serialization;

namespace Results;

public class ExceptionError : Error
{
    [JsonIgnore]
    public Exception Exception { get; private set; }

    public ExceptionError(Exception exception)
        : base(GetExceptionText(exception).ToString()) => this.Exception = exception;
}
