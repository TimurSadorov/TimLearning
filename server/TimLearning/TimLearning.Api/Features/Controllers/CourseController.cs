using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Attributes;
using TimLearning.Api.Consts;
using TimLearning.Api.Requests.Course;
using TimLearning.Api.Responses.Course;
using TimLearning.Application.UseCases.Courses.Commands.CreateCourse;
using TimLearning.Application.UseCases.Courses.Dto;
using TimLearning.Application.UseCases.Courses.Queries;
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

    [HttpPost("find")]
    [UserRoleAuthorize(UserRoleType.ContentCreator)]
    public async Task<List<FindCoursesResponse>> FindCourses( FindCoursesRequest request)
    {
        var courses = await _mediator.Send(
            new FindCourseQuery(request.Id, request.IsDraft, request.IsDeleted)
        );

        return courses
            .Select(
                c =>
                    new FindCoursesResponse(
                        c.Id,
                        c.Name,
                        c.ShortName,
                        c.Description,
                        c.IsDraft,
                        c.IsDeleted
                    )
            )
            .ToList();
    }

    [HttpGet("all")]
    public async Task<List<GetAllCoursesResponse>> GetAllCourses()
    {
        var courses = await _mediator.Send(new FindCourseQuery(IsDraft: false, IsDeleted: false));

        return courses.Select(c => new GetAllCoursesResponse(c.Id, c.Name, c.Description)).ToList();
    }
}
