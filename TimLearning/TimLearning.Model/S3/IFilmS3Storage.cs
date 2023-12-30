using TimLearning.Model.S3.Dto;

namespace TimLearning.Model.S3;

public interface IFilmS3Storage
{
    Task<Uri> UploadPictureAndGetAbsoluteUrlAsync(FileDto file);
}