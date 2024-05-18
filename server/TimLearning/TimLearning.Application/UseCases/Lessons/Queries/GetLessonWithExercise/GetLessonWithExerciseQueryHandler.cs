using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Services.ExerciseServices.Mappers;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Lessons.Queries.GetLessonWithExercise;

public class GetLessonWithExerciseQueryHandler
    : IRequestHandler<GetLessonWithExerciseQuery, LessonWithExerciseDto>
{
    private readonly IAppDbContext _dbContext;

    public GetLessonWithExerciseQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LessonWithExerciseDto> Handle(
        GetLessonWithExerciseQuery request,
        CancellationToken cancellationToken
    )
    {
        var lesson = await _dbContext
            .Lessons.Where(l => l.Id == request.LessonId)
            .Select(l => new LessonWithExerciseDto(
                l.Name,
                l.Text,
                l.Exercise == null ? null : l.Exercise.ToDto(null)
            ))
            .FirstOrDefaultAsync(cancellationToken);
        if (lesson is null)
        {
            throw new NotFoundException();
        }

        return lesson;
    }
}
