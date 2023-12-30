using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.Docker;

public record ImageSettingsRequest
{
    [Required]
    public required string Image { get; init; }
    
    [Required]
    public required string Tag { get; init; }

    [Required]
    public required ContainerSettingsRequest ContainerSettings { get; init; }
}
