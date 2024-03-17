using MediatR;
using TimLearning.Application.Data.ValueObjects;
using TimLearning.Application.Services.ModuleServices;
using TimLearning.Domain.Entities;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Modules.Commands.CreateModule;

public class CreateModuleCommandHandler : IRequestHandler<CreateModuleCommand>
{
    private readonly IAsyncSimpleValidator<CourseIdValueObject> _validator;
    private readonly IAppDbContext _dbContext;
    private readonly IModuleOrderService _moduleOrderService;

    public CreateModuleCommandHandler(
        IAsyncSimpleValidator<CourseIdValueObject> validator,
        IAppDbContext dbContext,
        IModuleOrderService moduleOrderService
    )
    {
        _validator = validator;
        _dbContext = dbContext;
        _moduleOrderService = moduleOrderService;
    }

    public async Task Handle(CreateModuleCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        await _validator.ValidateAndThrowAsync(
            new CourseIdValueObject(dto.CourseId),
            cancellationToken
        );

        var lastOrder = await _moduleOrderService.GetLastOrderAsync(
            dto.CourseId,
            cancellationToken
        );
        var newModule = new Module(lastOrder + 1 ?? 1)
        {
            Name = dto.Name,
            CourseId = dto.CourseId,
            IsDraft = true
        };
        _dbContext.Add(newModule);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
