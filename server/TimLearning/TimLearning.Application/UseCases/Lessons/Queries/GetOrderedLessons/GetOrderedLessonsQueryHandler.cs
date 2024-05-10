using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Specifications;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Exceptions;
using TimLearning.Domain.Services.LessonService;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Lessons.Queries.GetOrderedLessons;

public class GetOrderedLessonsQueryHandler
    : IRequestHandler<GetOrderedLessonsQuery, List<LessonSystemDataDto>>
{
    private readonly IAppDbContext _dbContext;
    private readonly ILessonOrderService _lessonOrderService;

    public GetOrderedLessonsQueryHandler(
        IAppDbContext dbContext,
        ILessonOrderService lessonOrderService
    )
    {
        _dbContext = dbContext;
        _lessonOrderService = lessonOrderService;
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
            .Where(l => l.ModuleId == request.ModuleId)
            .Where(LessonSpecifications.HasOrder)
            .Include(l => l.NextLesson)
            .Include(l => l.PreviousLesson)
            .ToListAsync(cancellationToken);

        return _lessonOrderService
            .Order(lessons)
            .Select(l => new LessonSystemDataDto(l.Id, l.Name, l.IsDraft))
            .ToList();
    }
}
