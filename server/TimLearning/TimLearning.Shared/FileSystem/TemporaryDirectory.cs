namespace TimLearning.Shared.FileSystem;

public class TemporaryDirectory : IDisposable
{
    public required DirectoryInfo Data { get; init; }

    public static TemporaryDirectory Create()
    {
        return new TemporaryDirectory { Data = Directory.CreateTempSubdirectory() };
    }

#pragma warning disable CA1816
    public void Dispose()
#pragma warning restore CA1816
    {
        try
        {
            Data.Delete(true);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
