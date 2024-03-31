using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;
using TimLearning.Api.Mappers.Lessons;
using TimLearning.Api.Requests.Lesson;
using TimLearning.Api.Responses.Lesson;
using TimLearning.Application.UseCases.Lessons.Command.CreateLesson;
using TimLearning.Application.UseCases.Lessons.Command.DeleteLesson;
using TimLearning.Application.UseCases.Lessons.Command.MoveLesson;
using TimLearning.Application.UseCases.Lessons.Command.RestoreLesson;
using TimLearning.Application.UseCases.Lessons.Command.UpdateLesson;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Application.UseCases.Lessons.Queries.GetDeletedLessons;
using TimLearning.Application.UseCases.Lessons.Queries.GetOrderedLessons;

namespace TimLearning.Api.Features.Controllers;

[Authorize]
[Route($"{ApiRoute.Prefix}")]
public class LessonController : SiteApiController
{
    private readonly IMediator _mediator;

    public LessonController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("modules/{moduleId:guid}/lessons/ordered")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<List<LessonSystemDataResponse>> GetOrderedLessons([FromRoute] Guid moduleId)
    {
        var modules = await _mediator.Send(new GetOrderedLessonsQuery(moduleId, UserId));

        return modules.Select(m => m.ToResponse()).ToList();
    }

    [HttpGet("modules/{moduleId:guid}/lessons/deleted")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<List<LessonSystemDataResponse>> GetDeletedLessons([FromRoute] Guid moduleId)
    {
        var modules = await _mediator.Send(new GetDeletedLessonsQuery(moduleId, UserId));

        return modules.Select(m => m.ToResponse()).ToList();
    }

    [HttpPost("modules/{moduleId:guid}/lessons")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task CreateLesson([FromRoute] Guid moduleId, [Required] CreateLessonRequest request)
    {
        return _mediator.Send(
            new CreateLessonCommand(new NewLessonDto(request.Name, moduleId), UserId)
        );
    }

    [HttpPatch("lessons/{lessonId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task UpdateLesson([FromRoute] Guid lessonId, [Required] UpdateLessonRequest request)
    {
        return _mediator.Send(
            new UpdateLessonCommand(
                new UpdatedLessonDto(lessonId, request.Name, request.Text, request.IsDraft),
                UserId
            )
        );
    }

    [HttpPatch("lessons/{lessonId:guid}/move")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task MoveLesson([FromRoute] Guid lessonId, [Required] MoveLessonRequest request)
    {
        return _mediator.Send(
            new MoveLessonCommand(new LessonMovementDto(lessonId, request.NextLessonId), UserId)
        );
    }

    [HttpPatch("lessons/{lessonId:guid}/delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task DeleteLesson([FromRoute] Guid lessonId)
    {
        return _mediator.Send(new DeleteLessonCommand(lessonId, UserId));
    }

    [HttpPatch("lessons/{lessonId:guid}/restore")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task RestoreLesson([FromRoute] Guid lessonId)
    {
        return _mediator.Send(new RestoreLessonCommand(lessonId, UserId));
    }
}
