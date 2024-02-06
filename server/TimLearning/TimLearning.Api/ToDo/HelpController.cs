using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Features.Controllers;

namespace TimLearning.Api.ToDo;

// TODO: For help, delete in prod
[Route("api/help-docker")]
public class HelpController : BaseController
{
    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        using var client = new DockerClientConfiguration().CreateClient();

        var list = await client.Networks.ListNetworksAsync(new NetworksListParameters());

        foreach (var networkResponse in list)
        {
            if (Guid.TryParse(networkResponse.Name, out _))
            {
                await client.Networks.DeleteNetworkAsync(networkResponse.ID);
            }
        }

        return Ok();
    }
}
