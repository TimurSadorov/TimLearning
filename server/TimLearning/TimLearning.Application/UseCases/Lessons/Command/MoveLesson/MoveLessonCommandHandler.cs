using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Specifications;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Lessons.Command.MoveLesson;

public class MoveLessonCommandHandler : IRequestHandler<MoveLessonCommand>
{
    private readonly IAppDbContext _dbContext;
    private readonly IAsyncSimpleValidator<LessonMovementDto> _lessonMovementValidator;

    public MoveLessonCommandHandler(
        IAppDbContext dbContext,
        IAsyncSimpleValidator<LessonMovementDto> lessonMovementValidator
    )
    {
        _dbContext = dbContext;
        _lessonMovementValidator = lessonMovementValidator;
    }

    public async Task Handle(MoveLessonCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        await _lessonMovementValidator.ValidateAndThrowAsync(dto, cancellationToken);

        var movableLesson = await _dbContext.Lessons
            .Include(l => l.PreviousLesson)
            .Include(l => l.NextLesson)
            .FirstAsync(l => l.Id == dto.LessonId, cancellationToken);
        if (movableLesson.NextLessonId == dto.NextLessonId)
        {
            return;
        }

        var newPreviousLesson = await _dbContext.Lessons
            .Include(l => l.NextLesson)
            .SingleOrDefaultAsync(
                LessonSpecifications.BeforeLesson(movableLesson.ModuleId, dto.NextLessonId),
                cancellationToken
            );
        if (movableLesson.PreviousLesson is not null)
        {
            var oldNextLesson = movableLesson.NextLesson;
            if (oldNextLesson is not null)
            {
                movableLesson.ClearNextValue();
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            movableLesson.PreviousLesson.SetNextLesson(oldNextLesson);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        var newNextLesson = newPreviousLesson?.NextLesson;
        if (newPreviousLesson is not null)
        {
            newPreviousLesson.SetNextLesson(movableLesson);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        else
        {
            newNextLesson = await _dbContext.Lessons.FirstAsync(
                l => l.Id == dto.NextLessonId,
                cancellationToken
            );
        }

        movableLesson.SetNextLesson(newNextLesson);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
