using TimLearning.Application.Services.ExerciseServices.Dto;
using TimLearning.DockerManager.ApiClient.V1.Response;

namespace TimLearning.Application.Services.ExerciseServices.Mappers;

public static class V1AppTestingStatusMapper
{
    public static ExerciseTestingStatus ToExerciseTestingStatus(this V1AppTestingStatus status)
    {
        return status switch
        {
            V1AppTestingStatus.Ok => ExerciseTestingStatus.Ok,
            V1AppTestingStatus.ErrorStartingServiceApp
                => ExerciseTestingStatus.ErrorStartingServiceApp,
            V1AppTestingStatus.ErrorExecutingMainApp => ExerciseTestingStatus.ErrorExecutingMainApp,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
    }
}
