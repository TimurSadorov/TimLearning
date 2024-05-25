using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Specifications;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Exceptions.Localized;

namespace TimLearning.Application.UseCases.UserProgresses.Commands.VisitLesson;

public class VisitLessonCommandHandler : IRequestHandler<VisitLessonCommand>
{
    private readonly IAppDbContext _dbContext;

    public VisitLessonCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(VisitLessonCommand request, CancellationToken cancellationToken)
    {
        if (
            await _dbContext
                .Lessons.Where(LessonSpecifications.UserAvailable)
                .AnyAsync(l => l.Id == request.LessonId, cancellationToken)
            is false
        )
        {
            LocalizedValidationException.ThrowSimpleTextError(
                "Пользовательский урок с таким индификатором не найден."
            );
        }

        if (
            await _dbContext.UserProgresses.AnyAsync(
                p => p.UserId == request.CallingUserId && p.LessonId == request.LessonId,
                cancellationToken
            )
        )
        {
            return;
        }

        var progress = new UserProgress
        {
            UserId = request.CallingUserId,
            LessonId = request.LessonId
        };
        if (
            await _dbContext
                .Lessons.Where(l => l.Id == request.LessonId)
                .Where(LessonSpecifications.IsPractical)
                .AnyAsync(cancellationToken)
            is false
        )
        {
            progress.Complete();
        }

        _dbContext.Add(progress);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
