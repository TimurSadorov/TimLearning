using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Application.Extensions;
using TimLearning.Application.UseCases.Modules.Dto;
using TimLearning.Infrastructure.Interfaces.Db;
using TimLearning.Shared.Validation.Validators;

namespace TimLearning.Application.UseCases.Modules.Commands.ChangeOrderModule;

public class ChangeModuleOrderCommandHandler : IRequestHandler<ChangeModuleOrderCommand>
{
    private readonly IAppDbContext _dbContext;
    private readonly IAsyncSimpleValidator<ModuleOrderChangingDto> _validator;

    public ChangeModuleOrderCommandHandler(
        IAppDbContext dbContext,
        IAsyncSimpleValidator<ModuleOrderChangingDto> validator
    )
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    public async Task Handle(ChangeModuleOrderCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        await _validator.ValidateAndThrowAsync(dto, cancellationToken);

        await _dbContext.Database.ExecuteInTransaction(
            async () =>
            {
                var module = await _dbContext.Modules.FirstAsync(
                    m => m.Id == dto.Id,
                    cancellationToken
                );
                var newOrder = dto.Order;
                if (module.Order == newOrder)
                {
                    return;
                }

                var maxOrder = Math.Max(newOrder, module.Order!.Value);
                var minOrder = Math.Min(newOrder, module.Order.Value);
                var offsetForOtherModules = newOrder < module.Order ? 1 : -1;

                await _dbContext.Modules
                    .Where(m => minOrder <= m.Order && m.Order <= maxOrder && m.Id != module.Id)
                    .ExecuteUpdateAsync(
                        setter =>
                            setter.SetProperty(m => m.Order, m => m.Order + offsetForOtherModules),
                        cancellationToken
                    );

                module.Order = dto.Order;
                await _dbContext.SaveChangesAsync(cancellationToken);
            },
            cancellationToken
        );
    }
}
