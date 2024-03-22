namespace TimLearning.Application.Services.ModuleServices;

public interface IModuleOrderService
{
    Task<int> GetNextOrderAsync(Guid courseId, CancellationToken ct = default);
}
