using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;
using TimLearning.Api.Requests.Module;
using TimLearning.Application.UseCases.Modules.Commands.CreateModule;
using TimLearning.Application.UseCases.Modules.Commands.Dto;

namespace TimLearning.Api.Features.Controllers;

[Route($"{ApiRoute.Prefix}/courses/{{courseId:guid}}/modules")]
[Authorize]
public class ModuleController : SiteApiController
{
    private readonly IMediator _mediator;

    public ModuleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task CreateModule([FromRoute] Guid courseId, [Required] CreateModuleRequest request)
    {
        return _mediator.Send(
            new CreateModuleCommand(new NewModuleDto(request.Name, courseId), UserId)
        );
    }
}
