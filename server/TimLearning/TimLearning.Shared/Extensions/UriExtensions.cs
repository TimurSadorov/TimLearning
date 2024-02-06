namespace TimLearning.Shared.Extensions;

public static class UriExtensions
{
    public static Uri Combine(this Uri baseUri, params string[] paths)
    {
        return new Uri(
            paths.Aggregate(
                baseUri.AbsoluteUri,
                (current, path) => $"{current.TrimEnd('/')}/{path.TrimStart('/')}"
            )
        );
    }
}
