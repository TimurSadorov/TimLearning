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

        if (movableLesson.PreviousLesson is not null)
        {
            var oldNextLessonId = movableLesson.NextLessonId;
            if (oldNextLessonId is not null)
            {
                movableLesson.ClearNextValue();
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            movableLesson.PreviousLesson.SetNextLesson(oldNextLessonId);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        var newPreviousLesson = await _dbContext.Lessons.FirstOrDefaultAsync(
            LessonSpecifications.BeforeLesson(movableLesson.ModuleId, dto.NextLessonId),
            cancellationToken
        );
        if (newPreviousLesson is not null)
        {
            newPreviousLesson.SetNextLesson(movableLesson.Id);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        movableLesson.SetNextLesson(dto.NextLessonId);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
