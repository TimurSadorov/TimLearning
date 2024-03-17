using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Modules.Commands.UpdateModule;

public class UpdateModuleCommandHandler : IRequestHandler<UpdateModuleCommand>
{
    private readonly IAppDbContext _dbContext;

    public UpdateModuleCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateModuleCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var module = await _dbContext.Modules.FirstOrDefaultAsync(
            m => m.Id == dto.Id,
            cancellationToken
        );
        if (module is null)
        {
            throw new NotFoundException();
        }

        if (dto.Name is not null)
        {
            module.Name = dto.Name;
        }
        if (dto.IsDraft is not null)
        {
            module.IsDraft = dto.IsDraft.Value;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
