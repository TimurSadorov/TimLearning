using TimLearning.DockerManager.Api.Services.Docker.Data;
using TimLearning.DockerManager.ApiClient.V1.Request;

namespace TimLearning.DockerManager.Api.Services.Mappers;

public static class V1ContainerEnvRequestMappers
{
    public static ContainerEnvDto ToDto(this V1ContainerEnvRequest request)
    {
        return new ContainerEnvDto(request.Name, request.Value);
    }
}
