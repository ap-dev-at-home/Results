namespace Results;

public class LogEntry
{
    public LogEntryType Type { get; private set; }

    public string Message { get; private set; }

    public DateTime Timestamp { get; private set; }

    public Exception? Exception { get; private set; }

    public LogEntry(string message, LogEntryType? logEntryType = null)
    {
        this.Type = logEntryType ?? LogEntryType.Info;
        this.Message = message;
        this.Timestamp = DateTime.UtcNow;
    }

    public LogEntry(Exception exception, string? message = null)
    {
        if (message != null)
        {
            this.Message = message;
        }
        else
        {
            this.Message = Error.GetExceptionText(exception).ToString();
        }

        this.Type = LogEntryType.Exception;
        this.Exception = exception;
    }
}
