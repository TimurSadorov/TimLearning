using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Specifications;
using TimLearning.Application.UseCases.Courses.Dto;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.UseCases.Courses.Queries.GetAllUserCourses;

public class GetAllUserCoursesQueryHandler
    : IRequestHandler<GetAllUserCoursesQuery, List<UserCourseDto>>
{
    private readonly IAppDbContext _dbContext;

    public GetAllUserCoursesQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<UserCourseDto>> Handle(
        GetAllUserCoursesQuery request,
        CancellationToken cancellationToken
    )
    {
        return _dbContext.Courses
            .Where(CourseSpecifications.UserAvailableCourses)
            .Select(c => new UserCourseDto(c.Id, c.Name, c.Description))
            .ToListAsync(cancellationToken);
    }
}
