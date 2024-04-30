using TimLearning.DockerManager.ApiClient.V1.Request;
using TimLearning.Domain.Entities.Data;

namespace TimLearning.Infrastructure.Implementation.Clients.DockerManager.Mappers;

public static class ContainerEnvDataMappers
{
    public static V1ContainerEnvRequest ToRequest(this ContainerEnvData data)
    {
        return new V1ContainerEnvRequest { Name = data.Name, Value = data.Value };
    }
}
