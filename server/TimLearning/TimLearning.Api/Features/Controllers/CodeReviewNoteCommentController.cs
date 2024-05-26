using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;
using TimLearning.Api.Requests.CodeReviewNoteComment;
using TimLearning.Application.UseCases.CodeReviewNoteComments.Commands.CreateCodeReviewNoteComment;
using TimLearning.Application.UseCases.CodeReviewNoteComments.Commands.DeleteCodeReviewNoteComment;
using TimLearning.Application.UseCases.CodeReviewNoteComments.Commands.UpdateCodeReviewNoteComment;
using TimLearning.Application.UseCases.CodeReviewNoteComments.Dto;

namespace TimLearning.Api.Features.Controllers;

[Authorize]
[Route($"{ApiRoute.Prefix}/code-reviews/notes")]
public class CodeReviewNoteCommentController : SiteApiController
{
    private readonly IMediator _mediator;

    public CodeReviewNoteCommentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{noteId:guid}/comments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task CreateCodeReviewNoteComment(
        [FromRoute] Guid noteId,
        [Required] CreateCodeReviewNoteCommentRequest request
    )
    {
        return _mediator.Send(
            new CreateCodeReviewNoteCommentCommand(
                new NewCodeReviewNoteCommentDto(noteId, request.Text),
                UserId
            )
        );
    }

    [HttpPatch("comments/{commentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task UpdateCodeReviewNoteComment(
        [FromRoute] Guid commentId,
        [Required] UpdateCodeReviewNoteCommentRequest request
    )
    {
        return _mediator.Send(
            new UpdateCodeReviewNoteCommentCommand(
                new UpdatedCodeReviewNoteCommentDto(commentId, request.Text),
                UserId
            )
        );
    }

    [HttpDelete("comments/{commentId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task DeleteCodeReviewNoteComment([FromRoute] Guid commentId)
    {
        return _mediator.Send(new DeleteCodeReviewNoteCommentCommand(commentId, UserId));
    }
}
