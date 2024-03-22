using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Services.ModuleServices;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Modules.Commands.RestoreModule;

public class RestoreModuleCommandHandler : IRequestHandler<RestoreModuleCommand>
{
    private readonly IAppDbContext _dbContext;
    private readonly IModuleOrderService _moduleOrderService;

    public RestoreModuleCommandHandler(
        IAppDbContext dbContext,
        IModuleOrderService moduleOrderService
    )
    {
        _dbContext = dbContext;
        _moduleOrderService = moduleOrderService;
    }

    public async Task Handle(RestoreModuleCommand request, CancellationToken cancellationToken)
    {
        var module = await _dbContext.Modules.FirstOrDefaultAsync(
            m => m.Id == request.ModuleId,
            cancellationToken
        );
        if (module is null)
        {
            throw new NotFoundException();
        }

        if (module.IsDeleted is false)
        {
            return;
        }

        var order = await _moduleOrderService.GetNextOrderAsync(module.CourseId, cancellationToken);
        module.Restore(order);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
