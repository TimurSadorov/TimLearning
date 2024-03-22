using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Specifications;
using TimLearning.Application.UseCases.Courses.Dto;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Courses.Queries.GetUserCourses;

public class GetUserCoursesQueryHandler
    : IRequestHandler<GetUserCoursesQuery, List<UserCourseDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetUserCoursesQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<UserCourseDto>> Handle(
        GetUserCoursesQuery request,
        CancellationToken cancellationToken
    )
    {
        var query = _dbContext.Courses.Where(CourseSpecifications.UserAvailableCourses);
        if (request.CourseId is not null)
        {
            query = query.Where(c => c.Id == request.CourseId);
        }

        return query
            .Select(c => new UserCourseDto(c.Id, c.Name, c.ShortName, c.Description))
            .ToListAsync(cancellationToken);
    }
}
