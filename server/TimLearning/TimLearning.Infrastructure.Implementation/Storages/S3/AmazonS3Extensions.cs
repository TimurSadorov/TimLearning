using Amazon.S3;

namespace TimLearning.Infrastructure.Implementation.Storages.S3;

public static class AmazonS3Extensions
{
    public static Protocol GetServiceUrlProtocol(this IAmazonS3 clientS3)
    {
        var scheme = new Uri(clientS3.Config.ServiceURL).Scheme;
        return scheme switch
        {
            "http" => Protocol.HTTP,
            "https" => Protocol.HTTPS,
            _ => throw new InvalidOperationException("Service url of S3 client has invalid scheme.")
        };
    }
}
