using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TimLearning.DockerManager.Api.Services;
using TimLearning.DockerManager.ApiClient.V1.Request;
using TimLearning.DockerManager.ApiClient.V1.Response;

namespace TimLearning.DockerManager.Api.Controllers;

[ApiController]
[Route("/api/v1/app")]
public class V1AppController : ControllerBase
{
    private readonly IAppTester _appTester;

    public V1AppController(IAppTester appTester)
    {
        _appTester = appTester;
    }

    [HttpPost("test")]
    [DisableRequestSizeLimit]
    public Task<V1AppTestResponse> Test(
        [FromForm] [Required] V1AppTestRequest request,
        CancellationToken ct
    )
    {
        return _appTester.Test(request, ct);
    }
}
