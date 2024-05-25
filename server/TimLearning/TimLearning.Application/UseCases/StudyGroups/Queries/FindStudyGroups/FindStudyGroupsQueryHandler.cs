using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.StudyGroups.Dto;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Extensions;

namespace TimLearning.Application.UseCases.StudyGroups.Queries.FindStudyGroups;

public class FindStudyGroupsQueryHandler
    : IRequestHandler<FindStudyGroupsQuery, List<StudyGroupDto>>
{
    private readonly IAppDbContext _dbContext;

    public FindStudyGroupsQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<StudyGroupDto>> Handle(
        FindStudyGroupsQuery request,
        CancellationToken cancellationToken
    )
    {
        var dto = request.Dto;

        var query = _dbContext.StudyGroups.Where(g => g.MentorId == request.CallingUserId);
        if (dto.Ids != null && dto.Ids.Count != 0)
        {
            query = query.Where(g => dto.Ids.Contains(g.Id));
        }

        if (dto.SearchName.IsNotNullOrEmpty())
        {
            var searchName = dto.SearchName.ToLower();
            query = query.Where(g => EF.Functions.Like(g.Name.ToLower(), $"%{searchName}%"));
        }

        if (dto.IsActive is not null)
        {
            query = query.Where(g => g.IsActive == dto.IsActive);
        }

        return query
            .Select(g => new StudyGroupDto(g.Id, g.Name, g.IsActive, g.CourseId))
            .ToListAsync(cancellationToken);
    }
}
