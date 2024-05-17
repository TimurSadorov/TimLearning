using System.Formats.Tar;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TimLearning.Application.Services.ExerciseServices.Dto;
using TimLearning.Application.Services.ExerciseServices.Mappers;
using TimLearning.Infrastructure.Interfaces.Clients.DockerManager;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Storages;
using TimLearning.Infrastructure.Interfaces.Storages.Mappers;
using TimLearning.Shared.FileSystem;
using TimLearning.Shared.Services.Archiving;

namespace TimLearning.Application.Services.ExerciseServices;

public class ExerciseTester : IExerciseTester
{
    private readonly IAppDbContext _dbContext;
    private readonly IFileStorage _fileStorage;
    private readonly IArchivingService _archivingService;
    private readonly ILogger<ExerciseTester> _logger;
    private readonly IDockerManagerClient _dockerManagerClient;

    public ExerciseTester(
        IAppDbContext dbContext,
        IFileStorage fileStorage,
        IArchivingService archivingService,
        ILogger<ExerciseTester> logger,
        IDockerManagerClient dockerManagerClient
    )
    {
        _dbContext = dbContext;
        _fileStorage = fileStorage;
        _archivingService = archivingService;
        _logger = logger;
        _dockerManagerClient = dockerManagerClient;
    }

    public async Task<ExerciseTestingResult> Test(ExerciseDto dto, CancellationToken ct = default)
    {
        var archiveFileDto = await _dbContext.StoredFiles
            .Where(f => f.Id == dto.AppArchiveId)
            .Select(f => f.ToStorageFileDto())
            .FirstAsync(ct);
        using var templateAppArchive = await _fileStorage.DownloadToTemporaryFile(
            archiveFileDto,
            ct
        );

        using var directoryWithTemplateApp = await TryExtractZipToTempDirectory(
            templateAppArchive,
            dto.AppArchiveId
        );
        if (directoryWithTemplateApp is null)
            return new ExerciseTestingResult(ExerciseTestingStatus.UnzippingError);

        var pathToFileForInsertCode = Path.Join(
            directoryWithTemplateApp.Data.FullName,
            Path.Join(dto.RelativePathToInsertCode)
        );
        if (File.Exists(pathToFileForInsertCode) is false)
            return new ExerciseTestingResult(ExerciseTestingStatus.FileByPathToInsertCodeNotFound);

        await File.WriteAllTextAsync(pathToFileForInsertCode, dto.InsertableCode, ct);

        using var appArchive = TemporaryFile.Create();
        // it is necessary that file does not exist for tar archiver
        appArchive.Data.Delete();
        await TarFile.CreateFromDirectoryAsync(
            directoryWithTemplateApp.Data.FullName,
            appArchive.Data.FullName,
            includeBaseDirectory: false,
            ct
        );

        var testingResult = await _dockerManagerClient.TestApp(
            appArchive.Data,
            dto.AppContainerData,
            dto.RelativePathToDockerfile,
            dto.ServiceApps,
            ct
        );

        return new ExerciseTestingResult(
            testingResult.Status.ToExerciseTestingStatus(),
            testingResult.ErrorMessage
        );
    }

    private async Task<TemporaryDirectory?> TryExtractZipToTempDirectory(
        TemporaryFile templateAppArchive,
        Guid appArchiveId
    )
    {
        try
        {
            return await _archivingService.ExtractZipToTempDirectory(
                templateAppArchive.Data,
                overwriteFiles: true
            );
        }
        catch (Exception exception)
        {
            _logger.LogWarning(
                exception,
                "Error when unzipping the application archive. StoredFileId: {StoredFileId}",
                appArchiveId
            );
            return null;
        }
    }
}
