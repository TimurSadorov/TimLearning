using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;
using TimLearning.Api.Requests.Course;
using TimLearning.Api.Responses.Course;
using TimLearning.Application.UseCases.Courses.Commands.CreateCourse;
using TimLearning.Application.UseCases.Courses.Commands.UpdateCourse;
using TimLearning.Application.UseCases.Courses.Dto;
using TimLearning.Application.UseCases.Courses.Queries.FindCourse;
using TimLearning.Application.UseCases.Courses.Queries.GetUserCourses;

namespace TimLearning.Api.Features.Controllers;

[Route($"{ApiRoute.Prefix}/courses")]
public class CourseController : SiteApiController
{
    private readonly IMediator _mediator;

    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("for-user")]
    public async Task<List<GetUserCoursesResponse>> GetUserCourses([FromQuery] Guid? courseId)
    {
        var courses = await _mediator.Send(new GetUserCoursesQuery(courseId));

        return courses
            .Select(c => new GetUserCoursesResponse(c.Id, c.Name, c.ShortName, c.Description))
            .ToList();
    }

    [Authorize]
    [HttpGet("all-info")]
    public async Task<List<FindCoursesResponse>> FindCourses([FromQuery] FindCoursesRequest request)
    {
        var courses = await _mediator.Send(
            new FindCourseQuery(
                UserId,
                request.Id,
                request.SearchName,
                request.IsDraft,
                request.IsDeleted
            )
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

    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateCourse([Required] CreateCourseRequest request)
    {
        await _mediator.Send(
            new CreateCourseCommand(
                new NewCourseDto(request.Name, request.ShortName, request.Description),
                UserId
            )
        );

        return Ok();
    }

    [Authorize]
    [HttpPatch("{courseId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCourse(
        [FromRoute] Guid courseId,
        [Required] UpdateCourseRequest request
    )
    {
        await _mediator.Send(
            new UpdateCourseCommand(
                new CourseUpdateDto(
                    courseId,
                    request.Name,
                    request.ShortName,
                    request.Description,
                    request.IsDraft,
                    request.IsDeleted
                ),
                UserId
            )
        );

        return Ok();
    }
}
