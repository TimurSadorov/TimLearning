using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimLearning.Infrastructure.Implementation.Storages.S3;
using TimLearning.Infrastructure.Interfaces.Storages;
using TimLearning.Shared.Configuration.Extensions;

namespace TimLearning.Infrastructure.Implementation.Storages;

public static class StoragesConfigurations
{
    public static void AddAllStorages(this IServiceCollection services, IConfiguration config)
    {
        services.AddRequiredOptions<AmazonS3Options>();
        services.AddAmazonS3Client(config.GetRequiredConfig<AmazonS3Options>());

        services.AddSingleton<IFileStorage, S3FileStorage>();
    }

    private static IServiceCollection AddAmazonS3Client(
        this IServiceCollection services,
        AmazonS3Options amazonS3Options
    )
    {
        var credentials = new BasicAWSCredentials(
            amazonS3Options.AccessKey,
            amazonS3Options.SecretKey
        );
        var config = new AmazonS3Config
        {
            ServiceURL = amazonS3Options.ServiceUrl,
            ForcePathStyle = true
        };
        var client = new AmazonS3Client(credentials, config);
        services.AddSingleton<IAmazonS3>(client);

        return services;
    }
}
