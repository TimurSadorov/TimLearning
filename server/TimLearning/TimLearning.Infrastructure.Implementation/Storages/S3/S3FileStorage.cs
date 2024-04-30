using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using TimLearning.Infrastructure.Interfaces.Storages;
using TimLearning.Shared.FileSystem;

namespace TimLearning.Infrastructure.Implementation.Storages.S3;

public class S3FileStorage : IFileStorage
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public S3FileStorage(IAmazonS3 s3Client, IOptions<AmazonS3Options> amazonS3SettingsOption)
    {
        _s3Client = s3Client;
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

    private static string GetKey(StoredFileDto dto)
    {
        return $"file/{dto.Added.Year}/{dto.Added.Month}/{dto.Added.Day}/{dto.Id}";
    }
}
