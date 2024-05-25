using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;
using TimLearning.Api.Requests.CodeReview;
using TimLearning.Api.Responses.CodeReview;
using TimLearning.Application.UseCases.CodeReviews.Dto;
using TimLearning.Application.UseCases.CodeReviews.Queries.GetStudyGroupCodeReviews;
using TimLearning.Application.UseCases.CodeReviews.Queries.GetUserSolutionCodeReview;

namespace TimLearning.Api.Features.Controllers;

[Authorize]
[Route($"{ApiRoute.Prefix}")]
public class CodeReviewController : SiteApiController
{
    private readonly IMediator _mediator;

    public CodeReviewController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("study-groups/{studyGroupId:guid}/code-reviews")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<List<GetStudyGroupCodeReviewsResponse>> GetStudyGroupCodeReviews(
        [FromRoute] Guid studyGroupId,
        [FromQuery] GetStudyGroupCodeReviewsRequest request
    )
    {
        var reviews = await _mediator.Send(
            new GetStudyGroupCodeReviewsQuery(
                new StudyGroupCodeReviewsFiltersDto(studyGroupId, request.Statuses),
                UserId
            )
        );

        return reviews
            .Select(r => new GetStudyGroupCodeReviewsResponse(
                r.Id,
                r.Status,
                r.Completed,
                r.UserEmail,
                r.ModuleName,
                r.LessonName
            ))
            .ToList();
    }

    [HttpGet("code-reviews/{codeReviewId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetUserSolutionCodeReviewResponse> GetUserSolutionCodeReview(
        [FromRoute] Guid codeReviewId
    )
    {
        var review = await _mediator.Send(new GetUserSolutionCodeReviewQuery(codeReviewId, UserId));

        return new GetUserSolutionCodeReviewResponse(
            review.Status,
            review.UserEmail,
            new CodeReviewLessonResponse(review.Lesson.Id, review.Lesson.Name, review.Lesson.Text),
            new UserSolutionResponse(review.Solution.Code, review.Solution.Added),
            review.StandardCode
        );
    }
}
