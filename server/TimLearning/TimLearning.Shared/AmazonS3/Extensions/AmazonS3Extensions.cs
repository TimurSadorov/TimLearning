using Amazon.S3;
using Amazon.S3.Util;

namespace TimLearning.Shared.AmazonS3.Extensions;

public static class AmazonS3Extensions
{
    public static async Task<bool> DoesS3BucketExistV2Async(
        this IAmazonS3 amazonS3,
        string bucketName
    )
    {
        return await AmazonS3Util.DoesS3BucketExistV2Async(amazonS3, bucketName);
    }
}
