using TimLearning.Api.Responses.Exercise;
using TimLearning.Domain.Entities.Data;

namespace TimLearning.Api.Mappers.Exercise;

public static class ContainerEnvDataMappers
{
    public static ContainerEnvResponse ToResponse(this ContainerEnvData data)
    {
        return new ContainerEnvResponse { Name = data.Name, Value = data.Value };
    }
}
