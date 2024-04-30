using TimLearning.Api.Requests.Exercise;
using TimLearning.Domain.Entities.Data;

namespace TimLearning.Api.Mappers.Exercise;

public static class ContainerEnvRequestMappers
{
    public static ContainerEnvData ToData(this ContainerEnvRequest request)
    {
        return new ContainerEnvData { Name = request.Name, Value = request.Value };
    }
}
