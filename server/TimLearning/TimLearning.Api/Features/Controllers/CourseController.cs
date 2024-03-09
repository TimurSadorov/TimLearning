using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Attributes;
using TimLearning.Api.Consts;
using TimLearning.Api.Requests.Course;
using TimLearning.Application.UseCases.Courses.Commands.CreateCourse;
using TimLearning.Application.UseCases.Courses.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Api.Features.Controllers;

[Route($"{ApiRoute.Prefix}/course")]
public class CourseController : SiteApiController
{
    private readonly IMediator _mediator;

    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [UserRoleAuthorize(UserRoleType.ContentCreator)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateCourse([Required] CreateCourseRequest request)
    {
        await _mediator.Send(
            new CreateCourseCommand(
                new NewCourseDto(request.Name, request.ShortName, request.Description)
            )
        );

        return Ok();
    }
}
