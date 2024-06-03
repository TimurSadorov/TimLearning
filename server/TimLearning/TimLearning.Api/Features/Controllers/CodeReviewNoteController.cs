using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;
using TimLearning.Api.Mappers.CodeReviewNote;
using TimLearning.Api.Requests.CodeReviewNote;
using TimLearning.Api.Responses.CodeReviewNote;
using TimLearning.Application.UseCases.CodeReviewNotes.Commands.CreateCodeReviewNote;
using TimLearning.Application.UseCases.CodeReviewNotes.Commands.DeleteCodeReviewNote;
using TimLearning.Application.UseCases.CodeReviewNotes.Dto;
using TimLearning.Application.UseCases.CodeReviewNotes.Queries.GetCodeReviewNotesWithComments;

namespace TimLearning.Api.Features.Controllers;

[Authorize]
[Route($"{ApiRoute.Prefix}/code-reviews")]
public class CodeReviewNoteController : SiteApiController
{
    private readonly IMediator _mediator;

    public CodeReviewNoteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{codeReviewId:guid}/notes-with-comments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<List<CodeReviewNoteWithCommentResponse>> GetCodeReviewNotesWithComments(
        [FromRoute] Guid codeReviewId
    )
    {
        var notes = await _mediator.Send(
            new GetCodeReviewNotesWithCommentsQuery(codeReviewId, UserId)
        );

        return notes
            .Select(n => new CodeReviewNoteWithCommentResponse(
                n.Id,
                n.StartPosition.ToResponse(),
                n.EndPosition.ToResponse(),
                n.Comments.Select(c => new CodeReviewNoteCommentResponse(
                        c.Id,
                        c.AuthorEmail,
                        c.Text,
                        c.Added
                    ))
                    .ToList()
            ))
            .ToList();
    }

    [HttpPost("{codeReviewId:guid}/notes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<CreateCodeReviewNoteResponse> CreateCodeReviewNote(
        [FromRoute] Guid codeReviewId,
        [Required] CreateCodeReviewNoteRequest request
    )
    {
        var noteId = await _mediator.Send(
            new CreateCodeReviewNoteCommand(
                new NewCodeReviewNote(
                    codeReviewId,
                    request.StartPosition.ToData(),
                    request.EndPosition.ToData(),
                    request.InitCommentText
                ),
                UserId
            )
        );

        return new CreateCodeReviewNoteResponse(noteId);
    }

    [HttpDelete("notes/{noteId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task DeleteCodeReviewNote([FromRoute] Guid noteId)
    {
        return _mediator.Send(new DeleteCodeReviewNoteCommand(noteId, UserId));
    }
}
