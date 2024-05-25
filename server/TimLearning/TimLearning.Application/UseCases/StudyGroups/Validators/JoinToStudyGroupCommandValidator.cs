using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Services.StudyGroupServices;
using TimLearning.Application.UseCases.StudyGroups.Commands.JoinToStudyGroup;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.StudyGroups.Validators;

public class JoinToStudyGroupCommandValidator : IAsyncSimpleValidator<JoinToStudyGroupCommand>
{
    private readonly IAppDbContext _dbContext;
    private readonly IStudyGroupDataEncryptor _studyGroupDataEncryptor;

    public JoinToStudyGroupCommandValidator(
        IAppDbContext dbContext,
        IStudyGroupDataEncryptor studyGroupDataEncryptor
    )
    {
        _dbContext = dbContext;
        _studyGroupDataEncryptor = studyGroupDataEncryptor;
    }

    public async Task ValidateAndThrowAsync(
        JoinToStudyGroupCommand entity,
        CancellationToken ct = default
    )
    {
        var dto = entity.Dto;
        var group = await _dbContext
            .StudyGroups.Where(g => g.Id == dto.GroupId)
            .Select(g => new { g.IsActive })
            .FirstOrDefaultAsync(ct);
        if (group is null)
        {
            throw new NotFoundException();
        }

        if (_studyGroupDataEncryptor.VerifyId(dto.GroupId, dto.Signature) is false)
        {
            LocalizedValidationException.ThrowSimpleTextError("Приглашение невалидно.");
        }

        if (group.IsActive is false)
        {
            LocalizedValidationException.ThrowSimpleTextError("Данная группа уже не активна.");
        }

        if (
            await _dbContext
                .GroupStudents.Where(s =>
                    s.StudyGroupId == dto.GroupId && s.UserId == entity.CallingUserId
                )
                .AnyAsync(ct)
        )
        {
            LocalizedValidationException.ThrowSimpleTextError("Вы уже состоите в этой группе.");
        }
    }
}
