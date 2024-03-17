using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Extensions;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Modules.Commands.DeleteModule;

public class DeleteModuleCommandHandler : IRequestHandler<DeleteModuleCommand>
{
    private readonly IAppDbContext _dbContext;

    public DeleteModuleCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteModuleCommand request, CancellationToken cancellationToken)
    {
        var module = await _dbContext.Modules.FirstOrDefaultAsync(
            m => m.Id == request.ModuleId,
            cancellationToken
        );
        if (module is null)
        {
            throw new NotFoundException();
        }

        if (module.IsDeleted)
        {
            return;
        }

        await _dbContext.Database.ExecuteInTransactionAsync(
            async () =>
            {
                var moduleOrder = module.Order;

                module.MarkDeleted();
                await _dbContext.SaveChangesAsync(cancellationToken);

                await _dbContext.Modules
                    .Where(m => m.CourseId == module.CourseId && m.Order > moduleOrder)
                    .ExecuteUpdateAsync(
                        setter => setter.SetProperty(m => m.Order, m => m.Order - 1),
                        cancellationToken
                    );
            },
            cancellationToken
        );
    }
}
