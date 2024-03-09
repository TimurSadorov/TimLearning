using TimLearning.Application.Services.CourseServices.Dto;
using TimLearning.Domain.Entities;

namespace TimLearning.Application.Services.CourseServices.Mappers;

public static class CourseCreateDtoMapper
{
    public static Course ToEntity(this CourseCreateDto dto)
    {
        return new Course
        {
            Name = dto.Name,
            ShortName = dto.ShortName,
            Description = dto.Description,
            IsDraft = dto.IsDraft,
            IsDeleted = dto.IsDeleted
        };
    }
}
