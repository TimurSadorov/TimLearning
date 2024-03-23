using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;
using TimLearning.Api.Requests.Module;
using TimLearning.Api.Responses.Module;
using TimLearning.Application.UseCases.Modules.Commands.ChangeOrderModule;
using TimLearning.Application.UseCases.Modules.Commands.CreateModule;
using TimLearning.Application.UseCases.Modules.Commands.DeleteModule;
using TimLearning.Application.UseCases.Modules.Commands.RestoreModule;
using TimLearning.Application.UseCases.Modules.Commands.UpdateModule;
using TimLearning.Application.UseCases.Modules.Dto;
using TimLearning.Application.UseCases.Modules.Queries.FindOrderedModules;

namespace TimLearning.Api.Features.Controllers;

[Route($"{ApiRoute.Prefix}")]
[Authorize]
public class ModuleController : SiteApiController
{
    private readonly IMediator _mediator;

    public ModuleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("courses/{courseId:guid}/modules/find")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<List<FindOrderedModulesResponse>> FindOrderedModules(
        [FromRoute] Guid courseId,
        [FromQuery] FindOrderedModulesRequest request
    )
    {
        var modules = await _mediator.Send(
            new FindOrderedModulesQuery(
                new ModulesFindDto(courseId, request.IsDeleted, request.IsDraft),
                UserId
            )
        );

        return modules
            .Select(m => new FindOrderedModulesResponse(m.Id, m.Name, m.IsDraft, m.IsDeleted))
            .ToList();
    }

    [HttpPost("courses/{courseId:guid}/modules")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task CreateModule([FromRoute] Guid courseId, [Required] CreateModuleRequest request)
    {
        return _mediator.Send(
            new CreateModuleCommand(new NewModuleDto(request.Name, courseId), UserId)
        );
    }

    [HttpPatch("modules/{moduleId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task UpdateModule([FromRoute] Guid moduleId, [Required] UpdateModuleRequest request)
    {
        return _mediator.Send(
            new UpdateModuleCommand(
                new UpdatedModuleDto(moduleId, request.Name, request.IsDraft),
                UserId
            )
        );
    }

    [HttpPatch("modules/{moduleId:guid}/order")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task ChangeModuleOrder(
        [FromRoute] Guid moduleId,
        [Required] ChangeModuleOrderRequest request
    )
    {
        return _mediator.Send(
            new ChangeModuleOrderCommand(
                new ModuleOrderChangingDto(moduleId, request.Order),
                UserId
            )
        );
    }

    [HttpPatch("modules/{moduleId:guid}/delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task DeleteModule([FromRoute] Guid moduleId)
    {
        return _mediator.Send(new DeleteModuleCommand(moduleId, UserId));
    }

    [HttpPatch("modules/{moduleId:guid}/restore")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task RestoreModule([FromRoute] Guid moduleId)
    {
        return _mediator.Send(new RestoreModuleCommand(moduleId, UserId));
    }
}
