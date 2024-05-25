using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Services.StudyGroupServices;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Infrastructure.Interfaces.Factories.Link;

namespace TimLearning.Application.UseCases.StudyGroups.Queries.GetLinkToJoin;

public class GetLinkToJoinQueryHandler : IRequestHandler<GetLinkToJoinQuery, string>
{
    private readonly IAppDbContext _dbContext;
    private readonly IStudyGroupDataEncryptor _studyGroupDataEncryptor;
    private readonly ITimLearningLinkFactory _timLearningLinkFactory;

    public GetLinkToJoinQueryHandler(
        IAppDbContext dbContext,
        IStudyGroupDataEncryptor studyGroupDataEncryptor,
        ITimLearningLinkFactory timLearningLinkFactory
    )
    {
        _dbContext = dbContext;
        _studyGroupDataEncryptor = studyGroupDataEncryptor;
        _timLearningLinkFactory = timLearningLinkFactory;
    }

    public async Task<string> Handle(
        GetLinkToJoinQuery request,
        CancellationToken cancellationToken
    )
    {
        var groupId = request.StudyGroupId;
        if (
            await _dbContext
                .StudyGroups.Where(g =>
                    g.MentorId == request.CallingUserId && g.Id == request.StudyGroupId
                )
                .AnyAsync(cancellationToken) == false
        )
        {
            throw new NotFoundException();
        }

        var sign = _studyGroupDataEncryptor.SingId(request.StudyGroupId);

        return _timLearningLinkFactory.GetLinkToJoinStudyGroup(groupId, sign);
    }
}
