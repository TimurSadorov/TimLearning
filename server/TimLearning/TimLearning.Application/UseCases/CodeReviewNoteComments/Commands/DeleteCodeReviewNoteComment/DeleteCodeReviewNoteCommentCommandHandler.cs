using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Specifications;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.CodeReviewNoteComments.Commands.DeleteCodeReviewNoteComment;

public class DeleteCodeReviewNoteCommentCommandHandler
    : IRequestHandler<DeleteCodeReviewNoteCommentCommand>
{
    private readonly IAppDbContext _dbContext;

    public DeleteCodeReviewNoteCommentCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(
        DeleteCodeReviewNoteCommentCommand request,
        CancellationToken cancellationToken
    )
    {
        var comment = await _dbContext
            .CodeReviewNoteComments.Where(
                CodeReviewNoteCommentSpecifications.AvailableToUser(request.CallingUserId)
            )
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (comment is null)
        {
            throw new NotFoundException();
        }

        comment.Delete();

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
