using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Entities;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Lessons.Queries.GetOrderedLessons;

public class GetOrderedLessonsQueryHandler
    : IRequestHandler<GetOrderedLessonsQuery, List<LessonSystemDataDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetOrderedLessonsQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<LessonSystemDataDto>> Handle(
        GetOrderedLessonsQuery request,
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

        var lessons = await _dbContext.Lessons
            .Where(l => l.ModuleId == request.ModuleId && l.IsDeleted == false)
            .Include(l => l.NextLesson)
            .Include(l => l.PreviousLesson)
            .ToListAsync(cancellationToken);

        return GetOrderedLessons(lessons)
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
