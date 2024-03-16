using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.UseCases.Modules.Commands.Dto;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Modules.Commands.CreateModule;

public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand>
{
    private readonly IAsyncSimpleValidator<NewModuleDto> _validator;
    private readonly IAppDbContext _dbContext;

    public CreateModuleCommandHandler(
        IAsyncSimpleValidator<NewModuleDto> validator,
        IAppDbContext dbContext
    )
    {
        _validator = validator;
        _dbContext = dbContext;
    }

    public async Task Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        await _validator.ValidateAndThrowAsync(dto, cancellationToken);

        var newModule = new Module { Name = dto.Name, CourseId = dto.CourseId };
        _dbContext.Add(newModule);

        var lastModule = await _dbContext.Modules
            .Where(m => m.CourseId == dto.CourseId && m.NextModule == null)
            .SingleOrDefaultAsync(cancellationToken);
        if (lastModule is not null)
        {
            lastModule.NextModule = newModule;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
