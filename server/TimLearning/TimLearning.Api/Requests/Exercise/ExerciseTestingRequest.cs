using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Requests.Exercise;

public class ExerciseTestingRequest
{
    [Required]
    public required string Code { get; init; }
}
