namespace TimLearning.Shared.Validation.Exceptions.Localized.Errors;

public class SimpleTextError(string message) : LocalizedError
{
    public string Message { get; } = message;
}
