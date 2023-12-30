namespace TimLearning.Model.Dto;

public record ExerciseCreateDto(DockerAppDto NewApp, string Code, string PathToRewriteFile)
{
    public List<ImageSettingsDto>? Images { get; init; }
}
