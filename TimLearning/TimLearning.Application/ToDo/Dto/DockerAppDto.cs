using Microsoft.AspNetCore.Http;

namespace TimLearning.Application.ToDo.Dto;

public record DockerAppDto(IFormFile Source, string PathToDockerfile, ContainerSettingsDto AppContainerSettings);
