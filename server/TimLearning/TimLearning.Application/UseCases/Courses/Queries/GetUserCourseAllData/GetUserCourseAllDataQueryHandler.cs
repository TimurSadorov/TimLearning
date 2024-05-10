using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Specifications;
using TimLearning.Application.UseCases.Courses.Dto;
using TimLearning.Domain.Entities.Enums;
using TimLearning.Domain.Exceptions;
using TimLearning.Domain.Services.LessonService;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.LinqSpecs;

namespace TimLearning.Application.UseCases.Courses.Queries.GetUserCourseAllData;

public class GetUserCourseAllDataQueryHandler
    : IRequestHandler<GetUserCourseAllDataQuery, UserCourseAllDataDto>
{
    private const int PercentageIfNoItems = 100;

    private readonly IAppDbContext _dbContext;
    private readonly ILessonOrderService _lessonOrderService;

    public GetUserCourseAllDataQueryHandler(
        IAppDbContext dbContext,
        ILessonOrderService lessonOrderService
    )
    {
        _dbContext = dbContext;
        _lessonOrderService = lessonOrderService;
    }

    public async Task<UserCourseAllDataDto> Handle(
        GetUserCourseAllDataQuery request,
        CancellationToken cancellationToken
    )
    {
        var course = await _dbContext.Courses
            .AsNoTracking()
            .Where(c => c.Id == request.CourseId)
            .Where(CourseSpecifications.UserAvailable)
            .Select(
                c =>
                    new
                    {
                        c.ShortName,
                        Modules = _dbContext.Modules
                            .Where(m => m.CourseId == c.Id)
                            .Where(ModuleSpecifications.UserAvailable)
                            .OrderBy(m => m.Order)
                            .Select(
                                m =>
                                    new
                                    {
                                        m.Id,
                                        m.Name,
                                        Lessons = _dbContext.Lessons
                                            .Where(l => l.ModuleId == m.Id)
                                            .Where(LessonSpecifications.HasOrder)
                                            .Include(l => l.NextLesson)
                                            .Include(l => l.PreviousLesson)
                                            .Include(
                                                l =>
                                                    l.UserProgresses.Where(
                                                        p => p.UserId == request.CallingUserId
                                                    )
                                            )
                                            .ToList()
                                    }
                            )
                            .ToList()
                    }
            )
            .FirstOrDefaultAsync(cancellationToken);
        if (course is null)
        {
            throw new NotFoundException();
        }

        var userAvailableOrderedLessonsByModuleIds = course.Modules.ToDictionary(
            m => m.Id,
            m =>
                _lessonOrderService
                    .Order(m.Lessons)
                    .Where(LessonSpecifications.UserAvailable.IsSatisfiedBy)
                    .Select(
                        l =>
                            new UserProgressInLessonDto(
                                l.Id,
                                l.Name,
                                l.UserProgresses.FirstOrDefault()?.Type
                            )
                    )
                    .ToList()
        );
        var completionPercentageByModuleIds = userAvailableOrderedLessonsByModuleIds.ToDictionary(
            pair => pair.Key,
            pair =>
                pair.Value.Count != 0
                    ? CalculatePercentage(
                        pair.Value.Count(l => l.UserProgress is UserProgressType.Completed),
                        pair.Value.Count
                    )
                    : PercentageIfNoItems
        );

        var courseCompletionPercentage =
            completionPercentageByModuleIds.Count != 0
                ? (int)completionPercentageByModuleIds.Select(p => p.Value).Average()
                : PercentageIfNoItems;

        return new UserCourseAllDataDto(
            course.ShortName,
            courseCompletionPercentage,
            course.Modules
                .Select(
                    m =>
                        new UserProgressInModuleDto(
                            m.Id,
                            m.Name,
                            completionPercentageByModuleIds[m.Id],
                            userAvailableOrderedLessonsByModuleIds[m.Id]
                        )
                )
                .ToList()
        );
    }

    private static int CalculatePercentage(int satisfyingNumber, int totalNumber) =>
        (int)((double)satisfyingNumber / totalNumber * 100);
}
