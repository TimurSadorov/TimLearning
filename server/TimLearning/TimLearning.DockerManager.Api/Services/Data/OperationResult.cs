using System.Diagnostics.CodeAnalysis;

namespace TimLearning.DockerManager.Api.Services.Data;

public class OperationResult
{
    [MemberNotNullWhen(false, nameof(ErrorMessage))]
    public bool IsSuccess { get; private set; }
    public string? ErrorMessage { get; private set; }

    private OperationResult() { }

    public static OperationResult Success()
    {
        return new OperationResult { IsSuccess = true };
    }

    public static OperationResult Error(string errorMessage)
    {
        return new OperationResult { IsSuccess = false, ErrorMessage = errorMessage };
    }
}

public class OperationResult<T>
{
    [MemberNotNullWhen(false, nameof(ErrorMessage))]
    [MemberNotNullWhen(false, nameof(Result))]
    public bool IsSuccess { get; private set; }
    public string? ErrorMessage { get; private set; }
    public T? Result { get; private set; }

    private OperationResult() { }

    public static OperationResult<T> Success(T result)
    {
        return new OperationResult<T> { IsSuccess = true, Result = result };
    }

    public static OperationResult<T> Error(string errorMessage)
    {
        return new OperationResult<T> { IsSuccess = false, ErrorMessage = errorMessage };
    }
}
