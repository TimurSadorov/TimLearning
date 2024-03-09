using TimLearning.Application.Services.CourseServices.Dto;

namespace TimLearning.Application.Services.CourseServices;

public interface ICourseEntityService
{
    Task<Guid> Create(CourseCreateDto dto);
    Task Update(CourseUpsertDto dto);
}
