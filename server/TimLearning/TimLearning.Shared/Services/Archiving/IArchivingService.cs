using TimLearning.Shared.FileSystem;

namespace TimLearning.Shared.Services.Archiving;

public interface IArchivingService
{
    Task<TemporaryDirectory> ExtractZipToTempDirectory(FileInfo archive, bool overwriteFiles);
}
