using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimLearning.Shared.Configuration.Extensions;

namespace TimLearning.Model.S3.Configuration;

public static class AmazonS3Configuration
{
    public static IServiceCollection AddAmazonS3(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var settings = configuration.GetRequiredSettings<AmazonS3Settings>();

        services.AddRequiredOptions<AmazonS3Settings>();
        services.AddAmazonS3Client(settings);

        return services;
    }

    private static IServiceCollection AddAmazonS3Client(
        this IServiceCollection services,
        AmazonS3Settings settings
    )
    {
        var credentials = new BasicAWSCredentials(settings.AccessKey, settings.SecretKey);
        var config = new AmazonS3Config { ForcePathStyle = true, ServiceURL = settings.ServiceUrl };
        var client = new AmazonS3Client(credentials, config);
        services.AddSingleton<IAmazonS3>(client);

        return services;
    }
}
