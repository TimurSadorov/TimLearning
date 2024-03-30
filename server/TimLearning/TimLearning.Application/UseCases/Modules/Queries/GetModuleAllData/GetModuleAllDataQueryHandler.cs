using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Modules.Dto;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Modules.Queries.GetModuleAllData;

public class GetModuleAllDataQueryHandler : IRequestHandler<GetModuleAllDataQuery, ModuleAllDataDto>
{
    private readonly IAppDbContext _dbContext;

    public GetModuleAllDataQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ModuleAllDataDto> Handle(
        GetModuleAllDataQuery request,
        CancellationToken cancellationToken
    )
    {
        var module = await _dbContext.Modules
            .Where(m => m.Id == request.ModuleId)
            .Select(
                m => new ModuleAllDataDto(m.Id, m.Name, m.Order, m.CourseId, m.IsDraft, m.IsDeleted)
            )
            .FirstOrDefaultAsync(cancellationToken);
        if (module is null)
        {
            throw new NotFoundException();
        }

        return module;
    }
}
