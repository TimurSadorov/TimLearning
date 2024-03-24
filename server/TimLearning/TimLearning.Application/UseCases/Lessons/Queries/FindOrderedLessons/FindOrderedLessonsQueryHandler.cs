using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Entities;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Lessons.Queries.FindOrderedLessons;

public class FindOrderedLessonsQueryHandler
    : IRequestHandler<FindOrderedLessonsQuery, List<LessonSystemDataDto>>
{
    private readonly IAppDbContext _dbContext;

    public FindOrderedLessonsQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<LessonSystemDataDto>> Handle(
        FindOrderedLessonsQuery request,
        CancellationToken cancellationToken
    )
    {
        var dto = request.Dto;
        if (
            await _dbContext.Modules.AnyAsync(m => m.Id == dto.ModuleId, cancellationToken) is false
        )
        {
            throw new NotFoundException();
        }

        var lessons = await _dbContext.Lessons
            .Where(l => l.ModuleId == dto.ModuleId && l.IsDeleted == false)
            .Include(l => l.NextLesson)
            .Include(l => l.PreviousLesson)
            .ToListAsync(cancellationToken);

        var orderedLessons = GetOrderedLessons(lessons);
        if (dto.IsDraft is not null)
        {
            orderedLessons = orderedLessons.Where(l => l.IsDraft == dto.IsDraft);
        }

        return orderedLessons
            .Select(l => new LessonSystemDataDto(l.Id, l.Name, l.IsDraft))
            .ToList();
    }

    private static IEnumerable<Lesson> GetOrderedLessons(List<Lesson> lessons)
    {
        var lesson = lessons.FirstOrDefault(l => l.PreviousLesson is null);
        while (lesson is not null)
        {
            yield return lesson;
            lesson = lesson.NextLesson;
        }
    }
}
