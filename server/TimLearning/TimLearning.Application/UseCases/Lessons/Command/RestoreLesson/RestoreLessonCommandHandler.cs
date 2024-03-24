using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Services.LessonServices;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Lessons.Command.RestoreLesson;

public class RestoreLessonCommandHandler : IRequestHandler<RestoreLessonCommand>
{
    private readonly IAppDbContext _dbContext;
    private readonly ILessonPositionService _lessonPositionService;

    public RestoreLessonCommandHandler(
        IAppDbContext dbContext,
        ILessonPositionService lessonPositionService
    )
    {
        _dbContext = dbContext;
        _lessonPositionService = lessonPositionService;
    }

    public async Task Handle(RestoreLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _dbContext.Lessons
            .Include(l => l.PreviousLesson)
            .FirstOrDefaultAsync(l => l.Id == request.LessonId, cancellationToken);
        if (lesson is null)
        {
            throw new NotFoundException();
        }

        if (lesson.IsDeleted is false)
        {
            return;
        }

        var lastLesson = await _lessonPositionService.FindLastLesson(
            lesson.ModuleId,
            cancellationToken
        );
        lesson.Restore(lastLesson);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
