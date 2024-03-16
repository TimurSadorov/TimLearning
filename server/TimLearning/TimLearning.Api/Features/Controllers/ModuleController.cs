using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;
using TimLearning.Api.Requests.Module;
using TimLearning.Api.Responses.Module;
using TimLearning.Application.UseCases.Modules.Commands.CreateModule;
using TimLearning.Application.UseCases.Modules.Dto;
using TimLearning.Application.UseCases.Modules.Queries.FindOrderedModules;

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

    [HttpPost("find")]
    public async Task<List<FindOrderedModulesResponse>> FindOrderedModules(
        [FromRoute] Guid courseId,
        [Required] FindOrderedModulesRequest request
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public Task CreateModule([FromRoute] Guid courseId, [Required] CreateModuleRequest request)
    {
        return _mediator.Send(
            new CreateModuleCommand(new NewModuleDto(request.Name, courseId), UserId)
        );
    }
}
