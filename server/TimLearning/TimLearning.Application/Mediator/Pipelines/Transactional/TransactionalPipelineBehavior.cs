using MediatR;
using TimLearning.Application.Extensions;
using TimLearning.Infrastructure.Interfaces.Db;

namespace TimLearning.Application.Mediator.Pipelines.Transactional;

public class TransactionPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest, ITransactional
{
    private readonly IAppDbContext _db;

    public TransactionPipelineBehavior(IAppDbContext db)
    {
        _db = db;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken ct
    )
    {
        var response = await _db.Database.ExecuteInTransactionAsync(
            () => next(),
            ct,
            TRequest.IsolationLevel
        );
        return response;
    }
}
