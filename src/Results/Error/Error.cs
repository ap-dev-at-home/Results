using System.Text;

namespace Results;

public class Error(string message)
{
    public string Message { get; private set; } = message;

    public static StringBuilder GetExceptionText(Exception ex, StringBuilder? sbParam = null)
    {
        var sb = sbParam ?? new StringBuilder();
        sb.Append(ex.GetType().Name + "\r\n" + ex.Message + "\r\n" + ex.StackTrace);
        if (ex.InnerException != null)
        {
            sb.AppendLine();
            GetExceptionText(ex.InnerException, sb);
        }

        return sb;
    }
}
