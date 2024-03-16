using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Data.ValueObjects;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Modules.Commands.CreateModule;

public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand>
{
    private readonly IAsyncSimpleValidator<CourseIdValueObject> _validator;
    private readonly IAppDbContext _dbContext;

    public CreateModuleCommandHandler(
        IAsyncSimpleValidator<CourseIdValueObject> validator,
        IAppDbContext dbContext
    )
    {
        _validator = validator;
        _dbContext = dbContext;
    }

    public async Task Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        await _validator.ValidateAndThrowAsync(
            new CourseIdValueObject(dto.CourseId),
            cancellationToken
        );

        var lastOrder = await _dbContext.Modules
            .Where(m => m.CourseId == dto.CourseId)
            .OrderByDescending(m => m.Order ?? 0)
            .Select(m => m.Order)
            .FirstOrDefaultAsync(cancellationToken);
        var newModule = new Module
        {
            Name = dto.Name,
            CourseId = dto.CourseId,
            Order = lastOrder + 1 ?? 1,
            IsDeleted = false,
            IsDraft = true
        };
        _dbContext.Add(newModule);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
