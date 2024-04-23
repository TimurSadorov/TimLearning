using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TimLearning.Api.Requests.StoredFile;

public class SaveExerciseAppFileRequest
{
    [Required]
    public required IFormFile File { get; init; }
}
