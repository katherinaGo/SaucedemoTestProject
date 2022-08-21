namespace Tests.Exceptions;

public class NoSuchDriverException : Exception
{
    public NoSuchDriverException(string? message) : base(message)
    {
    }
}