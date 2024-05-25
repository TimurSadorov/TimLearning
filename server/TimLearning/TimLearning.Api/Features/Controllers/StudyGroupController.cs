using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimLearning.Api.Consts;
using TimLearning.Api.Requests.StudyGroup;
using TimLearning.Api.Responses.StudyGroup;
using TimLearning.Application.UseCases.StudyGroups.Commands.CreateStudyGroup;
using TimLearning.Application.UseCases.StudyGroups.Commands.JoinToStudyGroup;
using TimLearning.Application.UseCases.StudyGroups.Commands.UpdateStudyGroup;
using TimLearning.Application.UseCases.StudyGroups.Dto;
using TimLearning.Application.UseCases.StudyGroups.Queries.FindStudyGroups;
using TimLearning.Application.UseCases.StudyGroups.Queries.GetLinkToJoin;

namespace TimLearning.Api.Features.Controllers;

[Authorize]
[Route($"{ApiRoute.Prefix}/study-groups")]
public class StudyGroupController : SiteApiController
{
    private readonly IMediator _mediator;

    public StudyGroupController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("find")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<List<StudyGroupResponse>> FindStudyGroups(
        [FromQuery] FindStudyGroupsRequest request
    )
    {
        var groups = await _mediator.Send(
            new FindStudyGroupsQuery(
                new StudyGroupsFindDto(request.Ids, request.SearchName, request.IsActive),
                UserId
            )
        );

        return groups
            .Select(g => new StudyGroupResponse(g.Id, g.Name, g.IsActive, g.CourseId))
            .ToList();
    }

    [HttpGet("${studyGroupId:guid}/link-to-join")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GetLinkToJoinToStudyGroupResponse> GetLinkToJoinToStudyGroup(
        [FromRoute] Guid studyGroupId
    )
    {
        var link = await _mediator.Send(new GetLinkToJoinQuery(studyGroupId, UserId));

        return new GetLinkToJoinToStudyGroupResponse(link);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<CreateStudyGroupResponse> CreateStudyGroup(
        [Required] CreateStudyGroupRequest request
    )
    {
        var groupId = await _mediator.Send(
            new CreateStudyGroupCommand(
                new NewStudyGroupDto(request.Name, request.CourseId),
                UserId
            )
        );

        return new CreateStudyGroupResponse(groupId);
    }

    [HttpPatch("${studyGroupId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task UpdateStudyGroup(
        [FromRoute] Guid studyGroupId,
        [Required] UpdateStudyGroupRequest request
    )
    {
        return _mediator.Send(
            new UpdateStudyGroupCommand(
                new UpdatableStudyGroupDto(studyGroupId, request.Name, request.IsActive),
                UserId
            )
        );
    }

    [HttpPost("${studyGroupId:guid}/join")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public Task JoinToStudyGroup(
        [FromRoute] Guid studyGroupId,
        [Required] JoinToStudyGroupRequest request
    )
    {
        return _mediator.Send(
            new JoinToStudyGroupCommand(new JoiningDataDto(studyGroupId, request.Signature), UserId)
        );
    }
}
