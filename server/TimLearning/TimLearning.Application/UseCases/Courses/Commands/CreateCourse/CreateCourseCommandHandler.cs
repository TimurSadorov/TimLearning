using MediatR;
using TimLearning.Application.Services.CourseServices;
using TimLearning.Application.Services.CourseServices.Dto;

namespace TimLearning.Application.UseCases.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand>
{
    private readonly ICourseEntityService _courseEntityService;

    public CreateCourseCommandHandler(ICourseEntityService courseEntityService)
    {
        _courseEntityService = courseEntityService;
    }

    public async Task Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        await _courseEntityService.Create(
            new CourseCreateDto(dto.Name, dto.ShortName, dto.Description, true, false)
        );
    }
}
