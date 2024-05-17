using System.ComponentModel.DataAnnotations;

namespace TimLearning.Api.Responses.Exercise;

public record ExerciseAppFileResponse([property: Required] string DownloadingUrl);
