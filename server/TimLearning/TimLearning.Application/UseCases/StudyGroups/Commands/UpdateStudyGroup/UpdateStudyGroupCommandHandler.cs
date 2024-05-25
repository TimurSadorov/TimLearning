using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Extensions;

namespace TimLearning.Application.UseCases.StudyGroups.Commands.UpdateStudyGroup;

public class UpdateStudyGroupCommandHandler : IRequestHandler<UpdateStudyGroupCommand>
{
    private readonly IAppDbContext _dbContext;

    public UpdateStudyGroupCommandHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateStudyGroupCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var group = await _dbContext
            .StudyGroups.Where(g => g.Id == dto.Id && g.MentorId == request.CallingUserId)
            .FirstOrDefaultAsync(cancellationToken);
        if (group is null)
        {
            throw new NotFoundException();
        }

        if (dto.Name.HasText())
        {
            group.Name = dto.Name;
        }

        if (dto.IsActive is not null)
        {
            group.IsActive = dto.IsActive.Value;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
