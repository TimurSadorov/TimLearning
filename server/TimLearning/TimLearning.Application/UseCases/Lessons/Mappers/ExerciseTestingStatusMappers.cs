using TimLearning.Application.Services.ExerciseServices.Dto;
using TimLearning.Application.UseCases.Lessons.Dto;

namespace TimLearning.Application.UseCases.Lessons.Mappers;

public static class ExerciseTestingStatusMappers
{
    public static UserExerciseTestingStatus ToUserTestingStatus(this ExerciseTestingStatus status)
    {
        return status switch
        {
            ExerciseTestingStatus.Ok => UserExerciseTestingStatus.Ok,
            ExerciseTestingStatus.UnzippingError
            or ExerciseTestingStatus.UnzippingError
            or ExerciseTestingStatus.FileByPathToInsertCodeNotFound
            or ExerciseTestingStatus.ErrorStartingServiceApp
                => UserExerciseTestingStatus.ServerError,
            ExerciseTestingStatus.ErrorExecutingMainApp => UserExerciseTestingStatus.UserError,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}
