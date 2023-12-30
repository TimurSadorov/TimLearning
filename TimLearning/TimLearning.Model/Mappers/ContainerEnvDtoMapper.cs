using TimLearning.Model.Dto;

namespace TimLearning.Model.Mappers;

public static class ContainerEnvDtoMapper
{
    public static string ToDockerEnv(this ContainerEnvDto env)
    {
        return $"{env.Name}={env.Value}";
    }
}
