using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.CodeReviews.Dto;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.CodeReviews.Queries.GetStudyGroupCodeReviews;

public class GetStudyGroupCodeReviewsQueryHandler
    : IRequestHandler<GetStudyGroupCodeReviewsQuery, List<StudyGroupCodeReviewDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetStudyGroupCodeReviewsQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<StudyGroupCodeReviewDto>> Handle(
        GetStudyGroupCodeReviewsQuery request,
        CancellationToken cancellationToken
    )
    {
        var dto = request.Dto;
        if (
            await _dbContext.StudyGroups.AnyAsync(
                g => g.Id == dto.StudyGroupId && g.MentorId == request.CallingUserId,
                cancellationToken
            )
            is false
        )
        {
            throw new NotFoundException();
        }

        var query = _dbContext.CodeReviews.Where(r =>
            r.GroupStudent.StudyGroupId == dto.StudyGroupId
        );

        if (dto.Statuses is not null && dto.Statuses.Count != 0)
        {
            query = query.Where(r => dto.Statuses.Contains(r.Status));
        }

        return await query
            .Select(r => new StudyGroupCodeReviewDto(
                r.Id,
                r.Status,
                r.Completed,
                r.GroupStudent.User.Email,
                r.UserSolution.Exercise.Lesson.Module.Name,
                r.UserSolution.Exercise.Lesson.Name
            ))
            .ToListAsync(cancellationToken);
    }
}
