﻿using MediatR;
using TimLearning.Application.Mediator.Pipelines.RoleAccess;
using TimLearning.Application.UseCases.Lessons.Dto;
using TimLearning.Domain.Entities.Enums;

namespace TimLearning.Application.UseCases.Lessons.Queries.FindOrderedLessons;

public record FindOrderedLessonsQuery(OrderedLessonFindDto Dto, Guid CallingUserId)
    : IRequest<List<LessonSystemDataDto>>,
        IAccessByRole
{
    public static IEnumerable<UserRoleType> ForRoles { get; } = [UserRoleType.ContentCreator];
}