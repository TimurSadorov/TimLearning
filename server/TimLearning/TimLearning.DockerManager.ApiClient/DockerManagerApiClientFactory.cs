using Microsoft.Extensions.Options;
using TimLearning.DockerManager.ApiClient.V1;

namespace TimLearning.DockerManager.ApiClient;

public class DockerManagerApiClientFactory : IDockerManagerApiClientFactory
{
    public const string ClientName = "DockerManager";

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly Uri _dockerManagerEndpoint;

    public DockerManagerApiClientFactory(
        IHttpClientFactory httpClientFactory,
        IOptions<DockerManagerApiOptions> apiOptions
    )
    {
        _httpClientFactory = httpClientFactory;
        _dockerManagerEndpoint = new Uri(apiOptions.Value.Url, UriKind.Absolute);
    }

    public V1DockerManagerApiClient CreateV1Client()
    {
        var httpClient = _httpClientFactory.CreateClient(ClientName);
        httpClient.BaseAddress = new Uri(_dockerManagerEndpoint, "api/v1/");
        httpClient.Timeout = TimeSpan.FromMinutes(5);

        return new V1DockerManagerApiClient(httpClient);
    }
}
