namespace TimLearning.Domain.Exceptions;

public class AccessException : Exception
{
    public AccessException(string message)
        : base(message) { }
}
