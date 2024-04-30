using TimLearning.Application.Services.ExerciseServices.Dto;

namespace TimLearning.Application.Services.ExerciseServices;

public interface IExerciseTester
{
    Task<ExerciseTestingResult> Test(ExerciseDto dto, CancellationToken ct = default);
}
