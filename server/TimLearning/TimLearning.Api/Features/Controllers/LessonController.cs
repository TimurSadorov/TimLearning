using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;
using TimLearning.Api.Mappers.Lessons;
using TimLearning.Api.Requests.Exercise;
using TimLearning.Api.Requests.Lesson;
using TimLearning.Api.Responses.Exercise;
using TimLearning.Api.Responses.Lesson;
using TimLearning.Application.UseCases.Lessons.Command.CreateLesson;
using TimLearning.Application.UseCases.Lessons.Command.DeleteLesson;
using TimLearning.Application.UseCases.Lessons.Command.MoveLesson;
using TimLearning.Application.UseCases.Lessons.Command.RestoreLesson;
using TimLearning.Application.UseCases.Lessons.Command.TestUserLessonExercise;
using TimLearning.Application.UseCases.Lessons.Command.UpdateLesson;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Application.UseCases.Lessons.Queries.GetDeletedLessons;
using TimLearning.Application.UseCases.Lessons.Queries.GetLessonWithExercise;
using TimLearning.Application.UseCases.Lessons.Queries.GetOrderedLessons;
using TimLearning.Application.UseCases.Lessons.Queries.GetUserLesson;
using TimLearning.Application.UseCases.Lessons.Queries.GetUserLessonExerciseAppFile;

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

    [HttpGet("lessons/{lessonId:guid}/with-exercise")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<LessonWithExerciseResponse> GetLessonWithExercise([FromRoute] Guid lessonId)
    {
        var lesson = await _mediator.Send(new GetLessonWithExerciseQuery(lessonId, UserId));

        return lesson.ToResponse();
    }

    [HttpGet("lessons/{lessonId:guid}/user-data")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<UserLessonResponse> GetUserLesson([FromRoute] Guid lessonId)
    {
        var lesson = await _mediator.Send(new GetUserLessonQuery(lessonId, UserId));

        return new UserLessonResponse(
            lesson.Id,
            lesson.Name,
            lesson.Text,
            lesson.CourseId,
            lesson.Exercise is null
                ? null
                : new UserExerciseResponse(lesson.Exercise.LastUserSolutionCode)
        );
    }

    [HttpGet("lessons/{lessonId:guid}/user-exercise-app")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ExerciseAppFileResponse> GetUserLessonExerciseAppFile(
        [FromRoute] Guid lessonId
    )
    {
        var lesson = await _mediator.Send(new GetUserLessonExerciseAppFileQuery(lessonId, UserId));

        return new ExerciseAppFileResponse(lesson.DownloadingUrl);
    }

    [HttpPost("lessons/{lessonId:guid}/user-exercise/test")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<UserExerciseTestingResponse> TestUserLessonExercise(
        [FromRoute] Guid lessonId,
        ExerciseTestingRequest request
    )
    {
        var lesson = await _mediator.Send(
            new TestUserLessonExerciseCommand(lessonId, request.Code, UserId)
        );

        return new UserExerciseTestingResponse(lesson.Status, lesson.ErrorMessage);
    }

    [HttpPatch("lessons/{lessonId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<UpdateLessonResponse> UpdateLesson(
        [FromRoute] Guid lessonId,
        [Required] UpdateLessonRequest request
    )
    {
        var result = await _mediator.Send(new UpdateLessonCommand(request.ToDto(lessonId), UserId));

        return new UpdateLessonResponse(
            result.IsSuccess,
            result.TestingResult is null
                ? null
                : new ExerciseTestingResultResponse(
                    result.TestingResult.Status,
                    result.TestingResult.ErrorMessage
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
