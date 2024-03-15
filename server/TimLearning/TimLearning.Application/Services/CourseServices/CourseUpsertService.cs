using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Services.CourseServices.Dto;
using TimLearning.Application.Services.CourseServices.Mappers;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Specifications.Dynamic;

namespace TimLearning.Application.Services.CourseServices;

public class CourseUpsertService : ICourseUpsertService
{
    private readonly IAppDbContext _dbContext;

    public CourseUpsertService(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Create(CourseCreateDto dto)
    {
        var course = dto.ToEntity();
        _dbContext.Add(course);
        await _dbContext.SaveChangesAsync();

        return course.Id;
    }

    public async Task Update(CourseUpsertDto dto)
    {
        var course = await _dbContext.Courses.SingleAsync(
            new EntityByGuidIdSpecification<Course>(dto.Id)
        );

        if (dto.Name is not null)
        {
            course.Name = dto.Name;
        }
        if (dto.ShortName is not null)
        {
            course.ShortName = dto.ShortName;
        }
        if (dto.Description is not null)
        {
            course.Description = dto.Description;
        }
        if (dto.IsDraft is not null)
        {
            course.IsDraft = dto.IsDraft.Value;
        }
        if (dto.IsDeleted is not null)
        {
            course.IsDeleted = dto.IsDeleted.Value;
        }

        await _dbContext.SaveChangesAsync();
    }
}
