using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;
using TimLearning.Api.Requests.UserProgress;
using TimLearning.Application.UseCases.UserProgresses.Commands.VisitLesson;

namespace TimLearning.Api.Features.Controllers;

[Authorize]
[Route($"{ApiRoute.Prefix}/user-progress")]
public class UserProgressController : SiteApiController
{
    private readonly IMediator _mediator;

    public UserProgressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("lesson-visiting")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task VisitLesson([Required] VisitLessonRequest request)
    {
        return _mediator.Send(new VisitLessonCommand(request.LessonId, UserId));
    }
}
