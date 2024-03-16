using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Data.ValueObjects;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.Validators.Course;

public class CourseIdValidator : IAsyncSimpleValidator<CourseIdValueObject>
{
    private readonly IAppDbContext _dbContext;

    public CourseIdValidator(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ValidateAndThrowAsync(
        CourseIdValueObject entity,
        CancellationToken ct = default
    )
    {
        if (await _dbContext.Courses.AnyAsync(c => c.Id == entity.Value, ct) == false)
        {
            throw new NotFoundException($"Course with id[{entity.Value}] not found.");
        }
    }
}
