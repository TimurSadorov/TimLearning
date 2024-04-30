using System.Net.Http.Json;
using TimLearning.DockerManager.ApiClient.V1.Dto;
using TimLearning.DockerManager.ApiClient.V1.Request;
using TimLearning.DockerManager.ApiClient.V1.Response;

namespace TimLearning.DockerManager.ApiClient.V1;

public class V1DockerManagerApiClient
{
    private readonly HttpClient _httpClient;

    internal V1DockerManagerApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<V1AppTestResponse> TestApp(V1TestAppDto dto, CancellationToken ct = default)
    {
        using var multipartFormContent = new MultipartFormDataContent();

        var fileStreamContent = new StreamContent(dto.AppArchive.OpenRead());
        multipartFormContent.Add(
            fileStreamContent,
            nameof(V1AppTestRequest.App),
            dto.AppArchive.Name
        );

        AddBaseContainerRequest(
            multipartFormContent,
            dto.AppContainer,
            nameof(V1AppTestRequest.AppContainer)
        );

        multipartFormContent.Add(
            new StringContent(dto.RelativePathToDockerfile),
            $"{nameof(V1AppTestRequest.RelativePathToDockerfile)}"
        );

        for (var i = 0; i < dto.ServiceApps.Count; i++)
        {
            var serviceParamName = $"{nameof(V1AppTestRequest.ServiceApps)}[{i}]";
            var service = dto.ServiceApps[i];
            multipartFormContent.Add(
                new StringContent(service.Name),
                $"{serviceParamName}.{nameof(V1ServiceContainerImageRequest.Name)}"
            );
            multipartFormContent.Add(
                new StringContent(service.Tag),
                $"{serviceParamName}.{nameof(V1ServiceContainerImageRequest.Tag)}"
            );

            var containerParamName =
                $"{serviceParamName}.{nameof(V1ServiceContainerImageRequest.Container)}";
            AddBaseContainerRequest(multipartFormContent, service.Container, containerParamName);

            if (service.Container.HealthcheckTest is not null)
            {
                AddList(
                    multipartFormContent,
                    service.Container.HealthcheckTest,
                    $"{containerParamName}.{nameof(V1ServiceContainerRequest.HealthcheckTest)}"
                );
            }
        }

        using var response = await _httpClient.PostAsync("app/test", multipartFormContent, ct);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<V1AppTestResponse>(ct)
            ?? throw new HttpContentException("Content was null.");
    }

    private static void AddBaseContainerRequest(
        MultipartFormDataContent multipartFormContent,
        V1ContainerRequest containerRequest,
        string baseParamName
    )
    {
        multipartFormContent.Add(
            new StringContent(containerRequest.Hostname ?? ""),
            $"{baseParamName}.{nameof(V1ContainerRequest.Hostname)}"
        );

        if (containerRequest.Envs is not null)
        {
            for (var i = 0; i < containerRequest.Envs.Count; i++)
            {
                var envParamName = $"{baseParamName}.{nameof(V1ContainerRequest.Envs)}[{i}]";
                var env = containerRequest.Envs[i];
                multipartFormContent.Add(
                    new StringContent(env.Name),
                    $"{envParamName}.{nameof(V1ContainerEnvRequest.Name)}"
                );
                multipartFormContent.Add(
                    new StringContent(env.Value),
                    $"{envParamName}.{nameof(V1ContainerEnvRequest.Value)}"
                );
            }
        }
    }

    private static void AddList<T>(
        MultipartFormDataContent multipartFormContent,
        IReadOnlyList<T> collection,
        string paramName
    )
    {
        for (int i = 0; i < collection.Count; i++)
        {
            AddValue(multipartFormContent, collection[i], $"{paramName}[{i}]");
        }
    }

    private static void AddValue<T>(
        MultipartFormDataContent multipartFormContent,
        T value,
        string paramName
    )
    {
        multipartFormContent.Add(
            new StringContent(value?.ToString() ?? throw new ArgumentNullException(nameof(value))),
            paramName
        );
    }
}
