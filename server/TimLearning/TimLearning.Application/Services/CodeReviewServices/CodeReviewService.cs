using Microsoft.EntityFrameworkCore;
using TimLearning.Domain.Entities;
using TimLearning.Domain.Entities.Enums;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.Services.CodeReviewServices;

public class CodeReviewService : ICodeReviewService
{
    private readonly IAppDbContext _dbContext;

    public CodeReviewService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddReviewsForUserSolution(Guid solutionId, CancellationToken ct = default)
    {
        var solution = await _dbContext
            .UserSolutions.Where(s => s.Id == solutionId)
            .Select(s => new
            {
                s.UserId,
                s.ExerciseId,
                s.Exercise.Lesson.Module.CourseId
            })
            .FirstAsync(ct);
        var studentIdsForUser = await _dbContext
            .GroupStudents.Where(s =>
                s.UserId == solution.UserId
                && s.StudyGroup.IsActive
                && s.StudyGroup.CourseId == solution.CourseId
            )
            .Select(s => s.Id)
            .ToListAsync(ct);
        var existedLastReviewsForUser = await _dbContext
            .CodeReviews.Include(r => r.UserSolution)
            .Where(r =>
                studentIdsForUser.Contains(r.GroupStudentId)
                && r.UserSolution.ExerciseId == solution.ExerciseId
            )
            .GroupBy(r => r.GroupStudentId)
            .Select(g => g.OrderByDescending(r => r.UserSolution.Added).First())
            .ToListAsync(ct);

        foreach (
            var review in existedLastReviewsForUser.Where(r => r.Status is CodeReviewStatus.Pending)
        )
        {
            review.SetUserSolutionId(solutionId);
        }

        AddNewForRejectedReviews(existedLastReviewsForUser, solutionId);

        AddReviewsCreatedForFirstTime(solutionId, studentIdsForUser, existedLastReviewsForUser);

        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<CodeReviewStatus> GetStatusAvailableToGroupMentor(
        Guid id,
        Guid mentorId,
        CancellationToken ct = default
    )
    {
        var review = await _dbContext
            .CodeReviews.Where(r => r.Id == id && r.GroupStudent.StudyGroup.MentorId == mentorId)
            .Select(r => new { r.Status })
            .FirstOrDefaultAsync(ct);
        if (review is null)
        {
            throw new NotFoundException();
        }

        return review.Status;
    }

    private void AddNewForRejectedReviews(
        List<CodeReview> existedLastReviewsForUser,
        Guid solutionId
    )
    {
        var newReviews = existedLastReviewsForUser
            .Where(r => r.Status is CodeReviewStatus.Rejected)
            .Select(r => new CodeReview(solutionId) { GroupStudentId = r.GroupStudentId });

        _dbContext.AddRange(newReviews);
    }

    private void AddReviewsCreatedForFirstTime(
        Guid solutionId,
        IEnumerable<Guid> studentIdsForUser,
        IEnumerable<CodeReview> existedLastReviewsForUser
    )
    {
        var newReviews = studentIdsForUser
            .Except(existedLastReviewsForUser.Select(r => r.GroupStudentId))
            .Select(studentId => new CodeReview(solutionId) { GroupStudentId = studentId });

        _dbContext.AddRange(newReviews);
    }
}
