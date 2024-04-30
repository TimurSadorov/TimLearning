using System.ComponentModel.DataAnnotations;
using TimLearning.Application.Services.ExerciseServices.Dto;

namespace TimLearning.Api.Responses.Exercise;

public record ExerciseTestingResultResponse(
    [property: Required] ExerciseTestingStatus Status,
    [property: Required] string? ErrorMessage
);
