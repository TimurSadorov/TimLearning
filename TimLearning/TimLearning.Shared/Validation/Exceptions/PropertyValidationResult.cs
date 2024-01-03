namespace TimLearning.Shared.Validation.Exceptions;

public record PropertyValidationResult(string Name, List<string> Errors);
