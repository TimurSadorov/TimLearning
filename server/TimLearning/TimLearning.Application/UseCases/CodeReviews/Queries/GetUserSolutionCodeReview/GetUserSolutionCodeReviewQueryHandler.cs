using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.CodeReviews.Dto;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.CodeReviews.Queries.GetUserSolutionCodeReview;

public class GetUserSolutionCodeReviewQueryHandler
    : IRequestHandler<GetUserSolutionCodeReviewQuery, UserSolutionCodeReviewDto>
{
    private readonly IAppDbContext _dbContext;

    public GetUserSolutionCodeReviewQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserSolutionCodeReviewDto> Handle(
        GetUserSolutionCodeReviewQuery request,
        CancellationToken cancellationToken
    )
    {
        var review = await _dbContext
            .CodeReviews.Where(r =>
                r.GroupStudent.StudyGroup.MentorId == request.CallingUserId
                && r.Id == request.CodeReviewId
            )
            .Select(r => new UserSolutionCodeReviewDto(
                r.Status,
                r.GroupStudent.User.Email,
                new CodeReviewLessonDto(
                    r.UserSolution.Exercise.Lesson.Id,
                    r.UserSolution.Exercise.Lesson.Name,
                    r.UserSolution.Exercise.Lesson.Text
                ),
                new UserSolutionDto(r.UserSolution.Code, r.UserSolution.Added),
                r.UserSolution.Exercise.StandardCode
            ))
            .FirstOrDefaultAsync(cancellationToken);
        if (review is null)
        {
            throw new NotFoundException();
        }

        return review;
    }
}
