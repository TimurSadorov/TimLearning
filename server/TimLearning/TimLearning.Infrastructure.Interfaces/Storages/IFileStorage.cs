namespace TimLearning.Infrastructure.Interfaces.Storages;

public interface IFileStorage
{
    Task UploadAsync(StoredFileDto dto, Stream file, string? contentType);
}
