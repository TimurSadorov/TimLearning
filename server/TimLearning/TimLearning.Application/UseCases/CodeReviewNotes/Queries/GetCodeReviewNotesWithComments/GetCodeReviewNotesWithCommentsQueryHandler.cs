using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.CodeReviewNotes.Dto;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.CodeReviewNotes.Queries.GetCodeReviewNotesWithComments;

public class GetCodeReviewNotesWithCommentsQueryHandler
    : IRequestHandler<GetCodeReviewNotesWithCommentsQuery, List<CodeReviewNoteWithCommentDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetCodeReviewNotesWithCommentsQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<CodeReviewNoteWithCommentDto>> Handle(
        GetCodeReviewNotesWithCommentsQuery request,
        CancellationToken cancellationToken
    )
    {
        var review = await _dbContext
            .CodeReviews.Where(r =>
                r.Id == request.CodeReviewId
                && (
                    r.GroupStudent.UserId == request.CallingUserId
                    || r.GroupStudent.StudyGroup.MentorId == request.CallingUserId
                )
            )
            .Select(r => new
            {
                Notes = r
                    .Notes.Where(n => n.Deleted == false)
                    .OrderBy(n => n.Added)
                    .Select(n => new CodeReviewNoteWithCommentDto(
                        n.Id,
                        n.StartPosition,
                        n.EndPosition,
                        n.Comments.Where(c => c.Deleted == false)
                            .OrderBy(c => c.Added)
                            .Select(c => new CodeReviewNoteCommentDto(
                                c.Id,
                                c.Author.Email,
                                c.Text,
                                c.Added
                            ))
                            .ToList()
                    ))
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
        if (review is null)
        {
            throw new NotFoundException();
        }

        return review.Notes;
    }
}
