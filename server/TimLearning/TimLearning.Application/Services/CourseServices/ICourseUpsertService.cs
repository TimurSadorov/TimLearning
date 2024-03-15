using TimLearning.Application.Services.CourseServices.Dto;

namespace TimLearning.Application.Services.CourseServices;

public interface ICourseUpsertService
{
    Task<Guid> Create(CourseCreateDto dto);
    Task Update(CourseUpsertDto dto);
}
