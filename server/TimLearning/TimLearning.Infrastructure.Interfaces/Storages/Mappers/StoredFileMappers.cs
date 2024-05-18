using TimLearning.Domain.Entities;

namespace TimLearning.Infrastructure.Interfaces.Storages.Mappers;

public static class StoredFileMappers
{
    public static StoredFileDto ToStorageFileDto(this StoredFile file)
    {
        return new StoredFileDto(file.Id, file.Added);
    }
}
