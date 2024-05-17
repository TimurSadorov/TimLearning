using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using TimLearning.Infrastructure.Interfaces.Providers.Clock;
using TimLearning.Infrastructure.Interfaces.Storages;
using TimLearning.Shared.FileSystem;

namespace TimLearning.Infrastructure.Implementation.Storages.S3;

public class S3FileStorage : IFileStorage
{
    private readonly IAmazonS3 _s3Client;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly string _bucketName;

    public S3FileStorage(
        IAmazonS3 s3Client,
        IOptions<AmazonS3Options> amazonS3SettingsOption,
        IDateTimeProvider dateTimeProvider
    )
    {
        _s3Client = s3Client;
        _dateTimeProvider = dateTimeProvider;
        _bucketName = amazonS3SettingsOption.Value.BucketName;
    }

    public async Task UploadAsync(
        StoredFileDto dto,
        Stream file,
        string? contentType,
        CancellationToken ct = default
    )
    {
        var key = GetKey(dto);
        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            ContentType = contentType,
            InputStream = file
        };

        await _s3Client.PutObjectAsync(request, ct);
    }

    public async Task<TemporaryFile> DownloadToTemporaryFile(
        StoredFileDto dto,
        CancellationToken ct = default
    )
    {
        var key = GetKey(dto);

        var objectResponse = await _s3Client.GetObjectAsync(_bucketName, key, ct);

        var tmpFile = TemporaryFile.Create();
        await using var fs = tmpFile.Data.OpenWrite();
        await objectResponse.ResponseStream.CopyToAsync(fs, ct);

        return tmpFile;
    }

    public Task<string> GetDownloadingUrl(
        StoredFileDto dto,
        int lifetimeInSec = 20,
        CancellationToken ct = default
    )
    {
        return GetDownloadingUrl(dto, TimeSpan.FromSeconds(lifetimeInSec), ct);
    }

    public async Task<string> GetDownloadingUrl(
        StoredFileDto dto,
        TimeSpan lifetime,
        CancellationToken ct = default
    )
    {
        var key = GetKey(dto);
        var now = await _dateTimeProvider.GetDateTimeUtcNow();
        var request = new GetPreSignedUrlRequest
        {
            Verb = HttpVerb.GET,
            BucketName = _bucketName,
            Key = key,
            Expires = now + lifetime,
            Protocol = _s3Client.GetServiceUrlProtocol()
        };

        return await _s3Client.GetPreSignedURLAsync(request);
    }

    private static string GetKey(StoredFileDto dto)
    {
        return $"file/{dto.Added.Year}/{dto.Added.Month}/{dto.Added.Day}/{dto.Id}";
    }
}
