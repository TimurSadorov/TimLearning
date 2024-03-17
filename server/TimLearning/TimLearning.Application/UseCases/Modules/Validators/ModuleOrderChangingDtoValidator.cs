using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Modules.Dto;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Exceptions.Localized;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Modules.Validators;

public class ModuleOrderChangingDtoValidator : IAsyncSimpleValidator<ModuleOrderChangingDto>
{
    private readonly IAppDbContext _dbContext;

    public ModuleOrderChangingDtoValidator(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ValidateAndThrowAsync(
        ModuleOrderChangingDto entity,
        CancellationToken ct = default
    )
    {
        var module = await _dbContext.Modules.FirstOrDefaultAsync(m => m.Id == entity.Id, ct);
        if (module is null)
        {
            throw new NotFoundException();
        }

        if (entity.Order <= 0)
        {
            LocalizedValidationException.ThrowSimpleTextError(
                "Значение порядка модуля должен быть больше нуля."
            );
        }
        if (module.IsDeleted)
        {
            LocalizedValidationException.ThrowSimpleTextError(
                "Невозможно сменить порядок модуля, который удален."
            );
        }

        var countModulesHavingOrder = await _dbContext.Modules.CountAsync(
            m => m.CourseId == module.CourseId && m.Order != null,
            ct
        );
        if (entity.Order > countModulesHavingOrder)
        {
            LocalizedValidationException.ThrowSimpleTextError(
                "Значение порядка модуля не может быть больше чем количество модулей, имеющие порядок."
            );
        }
    }
}
