using MediatR;
using TimLearning.Application.UseCases.Courses.Dto;

namespace TimLearning.Application.UseCases.Courses.Commands.CreateCourse;

public record CreateCourseCommand(NewCourseDto Dto) : IRequest;