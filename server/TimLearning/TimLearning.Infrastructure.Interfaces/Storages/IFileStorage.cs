using TimLearning.Shared.FileSystem;

namespace TimLearning.Infrastructure.Interfaces.Storages;

public interface IFileStorage
{
    Task UploadAsync(
        StoredFileDto dto,
        Stream file,
        string? contentType,
        CancellationToken ct = default
    );

    Task<TemporaryFile> DownloadToTemporaryFile(StoredFileDto dto, CancellationToken ct = default);
    
    Task<string> GetDownloadingUrl(StoredFileDto dto, int lifetimeInSec = 20, CancellationToken ct = default);
    
    Task<string> GetDownloadingUrl(StoredFileDto dto, TimeSpan lifetime, CancellationToken ct = default);
}
