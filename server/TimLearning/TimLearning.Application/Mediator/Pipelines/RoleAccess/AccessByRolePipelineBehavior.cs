using MediatR;
using Microsoft.EntityFrameworkCore;
using TimLearning.Domain.Exceptions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.Mediator.Pipelines.RoleAccess;

public class AccessByRolePipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IAccessByRole
{
    private readonly IAppDbContext _dbContext;

    public AccessByRolePipelineBehavior(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        var forRoles = TRequest.ForRoles;
        var hasAccess = await _dbContext.UserRoles
            .Where(r => r.UserId == request.CallingUserId)
            .AnyAsync(r => forRoles.Contains(r.Type), cancellationToken);
        if (hasAccess is false)
        {
            throw new AccessException(
                $"User with id[{request.CallingUserId}] has no access by role to action type[{typeof(TRequest).FullName}]."
            );
        }

        return await next();
    }
}
