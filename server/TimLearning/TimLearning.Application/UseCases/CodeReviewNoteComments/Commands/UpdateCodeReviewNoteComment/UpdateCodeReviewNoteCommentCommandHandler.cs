using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Specifications;
using TimLearning.Domain.Data.ValueObjects;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.CodeReviewNoteComments.Commands.UpdateCodeReviewNoteComment;

public class UpdateCodeReviewNoteCommentCommandHandler
    : IRequestHandler<UpdateCodeReviewNoteCommentCommand>
{
    private readonly IAppDbContext _dbContext;
    private readonly ISimpleValidator<CodeReviewNoteCommentTextValueObject> _textValidator;

    public UpdateCodeReviewNoteCommentCommandHandler(
        IAppDbContext dbContext,
        ISimpleValidator<CodeReviewNoteCommentTextValueObject> textValidator
    )
    {
        _dbContext = dbContext;
        _textValidator = textValidator;
    }

    public async Task Handle(
        UpdateCodeReviewNoteCommentCommand request,
        CancellationToken cancellationToken
    )
    {
        var dto = request.Dto;
        _textValidator.ValidateAndThrow(new CodeReviewNoteCommentTextValueObject(dto.Text));

        var comment = await _dbContext
            .CodeReviewNoteComments.Where(
                CodeReviewNoteCommentSpecifications.AvailableToUser(request.CallingUserId)
            )
            .FirstOrDefaultAsync(c => c.Id == request.Dto.Id, cancellationToken);
        if (comment is null)
        {
            throw new NotFoundException();
        }

        comment.Text = dto.Text;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
