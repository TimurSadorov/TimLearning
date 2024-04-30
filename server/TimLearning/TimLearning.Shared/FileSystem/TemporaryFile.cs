namespace TimLearning.Shared.FileSystem;

public class TemporaryFile : IDisposable
{
    public required FileInfo Data { get; init; }

    public static TemporaryFile Create()
    {
        return new TemporaryFile { Data = new FileInfo(Path.GetTempFileName()) };
    }

#pragma warning disable CA1816
    public void Dispose()
#pragma warning restore CA1816
    {
        try
        {
            Data.Delete();
        }
        catch (Exception)
        {
            // ignored
        }
    }
}
