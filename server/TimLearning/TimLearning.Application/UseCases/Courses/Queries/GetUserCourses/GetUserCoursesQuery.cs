using MediatR;
using TimLearning.Application.UseCases.Courses.Dto;

namespace TimLearning.Application.UseCases.Courses.Queries.GetUserCourses;

public record GetUserCoursesQuery(Guid? CourseId = null) : IRequest<List<UserCourseDto>>;
