using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Specifications;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Storages;
using TimLearning.Infrastructure.Interfaces.Storages.Mappers;

namespace TimLearning.Application.UseCases.Lessons.Queries.GetUserLessonExerciseAppFile;

public class GetUserLessonExerciseAppFileQueryHandler
    : IRequestHandler<GetUserLessonExerciseAppFileQuery, ExerciseAppFileDto>
{
    private readonly IAppDbContext _dbContext;
    private readonly IFileStorage _fileStorage;

    public GetUserLessonExerciseAppFileQueryHandler(
        IAppDbContext dbContext,
        IFileStorage fileStorage
    )
    {
        _dbContext = dbContext;
        _fileStorage = fileStorage;
    }

    public async Task<ExerciseAppFileDto> Handle(
        GetUserLessonExerciseAppFileQuery request,
        CancellationToken cancellationToken
    )
    {
        var storedFile = await _dbContext
            .Lessons.Where(l => l.Id == request.LessonId)
            .Where(LessonSpecifications.IsPractical && LessonSpecifications.UserAvailable)
            .Select(l => l.Exercise!.AppArchive.ToStorageFileDto())
            .FirstOrDefaultAsync(cancellationToken);
        if (storedFile is null)
        {
            throw new NotFoundException();
        }

        var downloadingUrl = await _fileStorage.GetDownloadingUrl(
            storedFile,
            ct: cancellationToken
        );
        return new ExerciseAppFileDto(downloadingUrl);
    }
}
