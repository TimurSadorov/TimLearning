using Docker.DotNet;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Mvc;

namespace TimLearning.Api.Features.Controllers;

// TODO: For help, delete in prod
[Route("api/help")]
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
