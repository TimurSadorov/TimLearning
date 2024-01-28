using TimLearning.Application.ToDo.Dto;

namespace TimLearning.Application.ToDo.Mappers;

public static class ContainerEnvDtoMapper
{
    public static string ToDockerEnv(this ContainerEnvDto env)
    {
        return $"{env.Name}={env.Value}";
    }
}
