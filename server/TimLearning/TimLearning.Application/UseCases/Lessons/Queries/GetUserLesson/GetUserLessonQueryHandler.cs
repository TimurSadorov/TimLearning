using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Specifications;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Lessons.Queries.GetUserLesson;

public class GetUserLessonQueryHandler : IRequestHandler<GetUserLessonQuery, UserLessonDto>
{
    private readonly IAppDbContext _dbContext;

    public GetUserLessonQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserLessonDto> Handle(
        GetUserLessonQuery request,
        CancellationToken cancellationToken
    )
    {
        var lesson = await _dbContext
            .Lessons.Where(l => l.Id == request.LessonId)
            .Where(LessonSpecifications.UserAvailable)
            .Select(l => new UserLessonDto(
                l.Id,
                l.Name,
                l.Text,
                l.Module.CourseId,
                l.Exercise != null
                    ? new UserExerciseDto(
                        l.Exercise.UserSolutions.OrderByDescending(s => s.Added)
                            .FirstOrDefault()!
                            .Code
                    )
                    : null
            ))
            .FirstOrDefaultAsync(cancellationToken);
        if (lesson is null)
        {
            throw new NotFoundException();
        }

        return lesson;
    }
}
