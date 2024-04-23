using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;
using TimLearning.Api.Requests.StoredFile;
using TimLearning.Application.UseCases.StoredFiles.Commands.SaveExerciseAppFile;
using TimLearning.Application.UseCases.StoredFiles.Dto;

namespace TimLearning.Api.Features.Controllers;

[Authorize]
[Route($"{ApiRoute.Prefix}/stored-files")]
public class StoredFileController : SiteApiController
{
    private readonly IMediator _mediator;

    public StoredFileController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("exercise-app")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<Guid> SaveExerciseAppFile(
        [FromForm] [Required] SaveExerciseAppFileRequest request
    )
    {
        var file = request.File;
        await using var fileStream = file.OpenReadStream();
        return await _mediator.Send(
            new SaveExerciseAppFileCommand(new FileDto(fileStream, file.ContentType), UserId)
        );
    }
}
