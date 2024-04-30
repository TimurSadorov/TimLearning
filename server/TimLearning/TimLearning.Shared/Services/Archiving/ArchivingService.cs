using System.IO.Compression;
using TimLearning.Shared.FileSystem;

namespace TimLearning.Shared.Services.Archiving;

public class ArchivingService : IArchivingService
{
    public async Task<TemporaryDirectory> ExtractZipToTempDirectory(
        FileInfo archive,
        bool overwriteFiles
    )
    {
        var tempDirectory = TemporaryDirectory.Create();
        await using var file = archive.OpenRead();
        ZipFile.ExtractToDirectory(file, tempDirectory.Data.FullName, overwriteFiles);

        return tempDirectory;
    }
}
