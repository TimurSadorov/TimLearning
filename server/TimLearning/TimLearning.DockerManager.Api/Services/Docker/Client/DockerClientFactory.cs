using Docker.DotNet;
using Microsoft.Extensions.Options;

namespace TimLearning.DockerManager.Api.Services.Docker.Client;

public class DockerClientFactory : IDockerClientFactory
{
    private readonly Uri _dockerUrl;

    public DockerClientFactory(IOptions<DockerOptions> dockerOptions)
    {
        _dockerUrl = new Uri(dockerOptions.Value.Url);
    }

    public IDockerClient Create()
    {
        return new DockerClientConfiguration(_dockerUrl).CreateClient();
    }
}
