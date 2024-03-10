using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Courses.Dto;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Specifications.Dynamic;

namespace TimLearning.Application.UseCases.Courses.Queries;

public class FindCourseQueryHandler : IRequestHandler<FindCourseQuery, List<CourseDto>>
{
    private readonly IAppDbContext _dbContext;

    public FindCourseQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<CourseDto>> Handle(
        FindCourseQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = _dbContext.Courses.AsQueryable();
        if (request.Id is not null)
        {
            query = query.Where(new EntityByGuidIdSpecification<Course>(request.Id.Value));
        }

        if (request.IsDeleted is not null)
        {
            query = query.Where(c => c.IsDeleted == request.IsDeleted);
        }
        if (request.IsDraft is not null)
        {
            query = query.Where(c => c.IsDraft == request.IsDraft);
        }

        return query
            .Select(
                c => new CourseDto(c.Id, c.Name, c.ShortName, c.Description, c.IsDraft, c.IsDeleted)
            )
            .ToListAsync(cancellationToken);
    }
}
