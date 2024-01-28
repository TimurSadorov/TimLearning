using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Features.Controllers;
using TimLearning.Api.Requests;
using TimLearning.Api.ToDo.Requests;
using TimLearning.Application.ToDo.Dto;
using TimLearning.Application.ToDoServices;

namespace TimLearning.Api.ToDo;

[Route("api/exercise")]
public class PracticeExerciseController : BaseController
{
    private readonly ExerciseService _exerciseService;

    public PracticeExerciseController(ExerciseService exerciseService)
    {
        _exerciseService = exerciseService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromForm] NewExerciseRequest request,
        CancellationToken ct
    )
    {
        var result = await _exerciseService.CreateAsync(
            new ExerciseCreateDto(
                new DockerAppDto(
                    request.NewApp.App,
                    request.NewApp.PathToDockerfile,
                    new ContainerSettingsDto()
                    {
                        Hostname = request.NewApp.ContainerSettings.Hostname,
                        HealthcheckTest = request.NewApp.ContainerSettings.HealthcheckTest,
                        Envs = request.NewApp.ContainerSettings.Envs
                            ?.Select(e => new ContainerEnvDto(e.Name, e.Value))
                            .ToList()
                    }
                ),
                request.Code,
                request.PathToRewriteFile
            )
            {
                Images = request.Images
                    ?.Select(
                        i =>
                            new ImageSettingsDto(
                                i.Image,
                                i.Tag,
                                new ContainerSettingsDto()
                                {
                                    Hostname = i.ContainerSettings.Hostname,
                                    HealthcheckTest = i.ContainerSettings.HealthcheckTest,
                                    Envs = i.ContainerSettings.Envs
                                        ?.Select(e => new ContainerEnvDto(e.Name, e.Value))
                                        .ToList()
                                }
                            )
                    )
                    .ToList()
            }
        );

        return Ok();
    }
}
