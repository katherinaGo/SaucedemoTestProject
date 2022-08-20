namespace Tests.Exceptions;

public class SuchUserDoesntExistException : Exception
{
    public SuchUserDoesntExistException(string? message) : base(message)
    {
    }
}