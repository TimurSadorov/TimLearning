namespace TimLearning.Application.Services.ModuleServices;

public interface IModuleOrderService
{
    Task<int?> GetLastOrderAsync(Guid courseId, CancellationToken ct = default);
}
