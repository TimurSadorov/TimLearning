using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using TimLearning.Infrastructure.Interfaces.Storages;

namespace TimLearning.Infrastructure.Implementation.Storages.S3;

public class S3FileStorage : IFileStorage
{
    private readonly IAmazonS3 _amazonS3;
    private readonly string _bucketName;

    public S3FileStorage(IAmazonS3 amazonS3, IOptions<AmazonS3Options> amazonS3SettingsOption)
    {
        _amazonS3 = amazonS3;
        _bucketName = amazonS3SettingsOption.Value.BucketName;
    }

    public async Task UploadAsync(StoredFileDto dto, Stream file, string? contentType)
    {
        var key = GetKey(dto);
        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            ContentType = contentType,
            InputStream = file
        };

        await _amazonS3.PutObjectAsync(request);
    }

    private static string GetKey(StoredFileDto dto)
    {
        return $"file/{dto.Added.Year}/{dto.Added.Month}/{dto.Added.Day}/{dto.Id}";
    }
}
