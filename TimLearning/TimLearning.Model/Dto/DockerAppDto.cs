using Microsoft.AspNetCore.Http;

namespace TimLearning.Model.Dto;

public record DockerAppDto(IFormFile Source, string PathToDockerfile, ContainerSettingsDto AppContainerSettings);
