using System.ComponentModel.DataAnnotations;
using TimLearning.Application.UseCases.Lessons.Dto;

namespace TimLearning.Api.Responses.Exercise;

public record UserExerciseTestingResponse(
    [property: Required] UserExerciseTestingStatus Status,
    string? ErrorMessage
);
