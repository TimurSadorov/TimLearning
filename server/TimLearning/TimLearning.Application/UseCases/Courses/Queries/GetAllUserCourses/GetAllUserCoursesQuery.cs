using MediatR;
using TimLearning.Application.UseCases.Courses.Dto;

namespace TimLearning.Application.UseCases.Courses.Queries.GetAllUserCourses;

public record GetAllUserCoursesQuery : IRequest<List<UserCourseDto>>;
