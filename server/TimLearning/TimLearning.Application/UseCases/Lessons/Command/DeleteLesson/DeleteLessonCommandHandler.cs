using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Lessons.Command.DeleteLesson;

public class DeleteLessonCommandHandler : IRequestHandler<DeleteLessonCommand>
{
    private readonly IAppDbContext _dbContext;

    public DeleteLessonCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteLessonCommand request, CancellationToken cancellationToken)
    {
        var lesson = await _dbContext.Lessons
            .Include(l => l.PreviousLesson)
            .FirstOrDefaultAsync(l => l.Id == request.LessonId, cancellationToken);
        if (lesson is null)
        {
            throw new NotFoundException();
        }

        if (lesson.IsDeleted)
        {
            return;
        }

        var nextLessonId = lesson.NextLessonId;
        lesson.Delete();
        await _dbContext.SaveChangesAsync(cancellationToken);

        if (lesson.PreviousLesson is not null)
        {
            lesson.PreviousLesson.SetNextLesson(nextLessonId);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
