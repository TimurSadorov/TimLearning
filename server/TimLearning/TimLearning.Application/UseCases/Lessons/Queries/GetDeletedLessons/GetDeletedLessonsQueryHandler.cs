using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Lessons.Queries.GetDeletedLessons;

public class GetDeletedLessonsQueryHandler
    : IRequestHandler<GetDeletedLessonsQuery, List<LessonSystemDataDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetDeletedLessonsQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<LessonSystemDataDto>> Handle(
        GetDeletedLessonsQuery request,
        CancellationToken cancellationToken
    )
    {
        if (
            await _dbContext.Modules.AnyAsync(m => m.Id == request.ModuleId, cancellationToken)
            is false
        )
        {
            throw new NotFoundException();
        }

        return await _dbContext.Lessons
            .Where(l => l.ModuleId == request.ModuleId && l.IsDeleted)
            .Select(l => new LessonSystemDataDto(l.Id, l.Name, l.IsDraft))
            .ToListAsync(cancellationToken);
    }
}
