using MediatR;
using TimLearning.Application.UseCases.Courses.Dto;

namespace TimLearning.Application.UseCases.Courses.Queries;

public record FindCourseQuery(Guid? Id = null, bool? IsDraft = null, bool? IsDeleted = null) : IRequest<List<CourseDto>>;
