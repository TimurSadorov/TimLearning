using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace TimLearning.Application.Extensions;

public static class DatabaseFacadeExtensions
{
    private const IsolationLevel DefaultIsolationLevel = IsolationLevel.ReadCommitted;

    public static Task ExecuteInTransactionAsync(
        this DatabaseFacade database,
        Func<Task> operation,
        CancellationToken cancellationToken = default,
        IsolationLevel isolationLevel = DefaultIsolationLevel
    )
    {
        return database.ExecuteInTransactionAsync(
            async () =>
            {
                await operation();
                return 0;
            },
            cancellationToken,
            isolationLevel
        );
    }

    public static Task<TResult> ExecuteInTransactionAsync<TResult>(
        this DatabaseFacade database,
        Func<Task<TResult>> operation,
        CancellationToken cancellationToken = default,
        IsolationLevel isolationLevel = DefaultIsolationLevel
    )
    {
        var currentTransaction = database.CurrentTransaction;
        if (currentTransaction is not null)
        {
            if (currentTransaction.GetDbTransaction().IsolationLevel != isolationLevel)
            {
                throw new InvalidOperationException(
                    "Transaction with a different isolation level is in progress."
                );
            }

            return operation();
        }

        var executionStrategy = database.CreateExecutionStrategy();
        return executionStrategy.ExecuteAsync(async () =>
        {
            await using var transaction = await database.BeginTransactionAsync(
                isolationLevel,
                cancellationToken
            );

            TResult? result;
            try
            {
                result = await operation();
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }

            await transaction.CommitAsync(cancellationToken);
            return result;
        });
    }
}
