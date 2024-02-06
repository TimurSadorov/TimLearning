namespace TimLearning.Application.ToDo.Dto;

public record ExerciseCreateDto(DockerAppDto NewApp, string Code, string PathToRewriteFile)
{
    public List<ImageSettingsDto>? Images { get; init; }
}
