using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TimLearning.Model.S3.Configuration;
using TimLearning.Model.S3.Dto;
using TimLearning.Shared.AmazonS3.Extensions;
using TimLearning.Shared.Extensions;

namespace TimLearning.Model.S3;

public class FilmS3Storage : IFilmS3Storage
{
    private readonly IAmazonS3 _amazonS3;
    private readonly string _bucketName;
    private readonly ILogger<FilmS3Storage> _logger;

    public FilmS3Storage(
        IAmazonS3 amazonS3,
        IOptions<AmazonS3Settings> amazonS3SettingsOption,
        ILogger<FilmS3Storage> logger
    )
    {
        _amazonS3 = amazonS3;
        _logger = logger;
        _bucketName = amazonS3SettingsOption.Value.ExercisesBucketName;
    }

    public async Task<Uri> UploadPictureAndGetAbsoluteUrlAsync(FileDto file)
    {
        await ThrowExceptionIfBucketDoesNotExistAndLogCriticalAsync();

        var key = GetKeyForFilmPicture(Guid.NewGuid());
        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            ContentType = file.ContentType,
            InputStream = file.Content
        };
        await _amazonS3.PutObjectAsync(request);

        return new Uri(_amazonS3.Config.ServiceURL).Combine(_bucketName, key);
    }

    private static string GetKeyForFilmPicture(Guid idFile)
    {
        return $"{idFile}";
    }

    private async Task ThrowExceptionIfBucketDoesNotExistAndLogCriticalAsync()
    {
        if (!await _amazonS3.DoesS3BucketExistV2Async(_bucketName))
        {
            _logger.LogCritical(
                $"The bucket with the name: \"{_bucketName}\" does not exist. "
                    + "It is necessary to create a bucket with your hands in S3."
            );
            throw new InvalidOperationException(
                $"A bucket named \"{_bucketName}\" does not exist."
            );
        }
    }
}
