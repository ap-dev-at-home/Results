namespace Results;

public class InterlockError : Error
{
    public InterlockError()
        : base("Lock can not be aquired.")
    {
    }
}
