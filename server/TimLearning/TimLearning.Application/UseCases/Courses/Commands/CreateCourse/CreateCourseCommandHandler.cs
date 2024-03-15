using MediatR;
using TimLearning.Application.Services.CourseServices;
using TimLearning.Application.Services.CourseServices.Dto;

namespace TimLearning.Application.UseCases.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand>
{
    private readonly ICourseUpsertService _courseUpsertService;

    public CreateCourseCommandHandler(ICourseUpsertService courseUpsertService)
    {
        _courseUpsertService = courseUpsertService;
    }

    public async Task Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        await _courseUpsertService.Create(
            new CourseCreateDto(dto.Name, dto.ShortName, dto.Description, true, false)
        );
    }
}
