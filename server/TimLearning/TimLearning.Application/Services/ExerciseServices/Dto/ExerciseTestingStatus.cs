namespace TimLearning.Application.Services.ExerciseServices.Dto;

public enum ExerciseTestingStatus
{
    Ok,
    UnzippingError,
    FileByPathToInsertCodeNotFound,
    ErrorStartingServiceApp,
    ErrorExecutingMainApp
}
