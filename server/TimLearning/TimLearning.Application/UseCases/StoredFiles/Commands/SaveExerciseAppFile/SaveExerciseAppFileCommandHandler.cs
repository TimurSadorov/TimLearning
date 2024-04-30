using MediatR;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;
using TimLearning.Infrastructure.Interfaces.Storages;
using TimLearning.Shared.Validation.Exceptions.Localized;

namespace TimLearning.Application.UseCases.StoredFiles.Commands.SaveExerciseAppFile;

public class SaveExerciseAppFileCommandHandler : IRequestHandler<SaveExerciseAppFileCommand, Guid>
{
    private const long MaxSizeByte = 1024 * 1024 * 200;

    private readonly IAppDbContext _dbContext;
    private readonly IFileStorage _fileStorage;
    private readonly IDateTimeProvider _dateTimeProvider;

    public SaveExerciseAppFileCommandHandler(
        IAppDbContext dbContext,
        IFileStorage fileStorage,
        IDateTimeProvider dateTimeProvider
    )
    {
        _dbContext = dbContext;
        _fileStorage = fileStorage;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Guid> Handle(
        SaveExerciseAppFileCommand request,
        CancellationToken cancellationToken
    )
    {
        var fileDto = request.FileDto;
        if (fileDto.File.Length > MaxSizeByte)
        {
            LocalizedValidationException.ThrowSimpleTextError("Размер файла слишком большой.");
        }

        var storedFile = new StoredFile
        {
            AddedById = request.CallingUserId,
            Added = await _dateTimeProvider.GetUtcNow()
        };
        _dbContext.Add(storedFile);

        await _fileStorage.UploadAsync(
            new StoredFileDto(storedFile.Id, storedFile.Added),
            fileDto.File,
            fileDto.MimeType,
            cancellationToken
        );

        await _dbContext.SaveChangesAsync(cancellationToken);

        return storedFile.Id;
    }
}
