using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Data.ValueObjects;
using TimLearning.Application.UseCases.Modules.Dto;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Modules.Queries.FindOrderedModules;

public class FindOrderedModulesQueryHandler
    : IRequestHandler<FindOrderedModulesQuery, List<OrderedModuleDto>>
{
    private readonly IAppDbContext _dbContext;
    private readonly IAsyncSimpleValidator<CourseIdValueObject> _courseValidator;

    public FindOrderedModulesQueryHandler(
        IAppDbContext dbContext,
        IAsyncSimpleValidator<CourseIdValueObject> courseValidator
    )
    {
        _dbContext = dbContext;
        _courseValidator = courseValidator;
    }

    public async Task<List<OrderedModuleDto>> Handle(
        FindOrderedModulesQuery request,
        CancellationToken cancellationToken
    )
    {
        var dto = request.Dto;
        await _courseValidator.ValidateAndThrowAsync(
            new CourseIdValueObject(dto.CourseId),
            cancellationToken
        );

        var query = _dbContext.Modules.Where(
            m => m.CourseId == dto.CourseId && m.IsDeleted == dto.IsDeleted
        );

        if (dto.IsDraft is not null)
        {
            query = query.Where(m => m.IsDraft == dto.IsDraft);
        }

        return await query
            .OrderBy(m => m.Order)
            .Select(m => new OrderedModuleDto(m.Id, m.Name, m.IsDraft, m.IsDeleted))
            .ToListAsync(cancellationToken);
    }
}
