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
                    ? l.Exercise.UserSolutions.Where(s => s.UserId == request.CallingUserId)
                        .OrderByDescending(s => s.Added)
                        .Select(s => new UserSolutionDto(
                            s.Code,
                            s.CodeReview != null
                                ? new UserCodeReviewDto(s.CodeReview.Id, s.CodeReview.Status)
                                : null
                        ))
                        .FirstOrDefault() ?? new UserSolutionDto(null, null)
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
