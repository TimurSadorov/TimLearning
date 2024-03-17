using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace TimLearning.Application.Extensions;

public static class DatabaseFacadeExtensions
{
    private const IsolationLevel DefaultIsolationLevel = IsolationLevel.ReadCommitted;

    public static async Task ExecuteInTransaction(
        this DatabaseFacade database,
        Func<Task> operation,
        CancellationToken cancellationToken = default,
        IsolationLevel isolationLevel = DefaultIsolationLevel
    )
    {
        await database.ExecuteInTransaction(
            async () =>
            {
                await operation();
                return 0;
            },
            cancellationToken,
            isolationLevel
        );
    }

    public static async Task<TResult> ExecuteInTransaction<TResult>(
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

            return await operation();
        }

        var executionStrategy = database.CreateExecutionStrategy();
        return await executionStrategy.ExecuteAsync(async () =>
        {
            await using var transaction = await database.BeginTransactionAsync(
                isolationLevel,
                cancellationToken
            );
            try
            {
                var result = await operation();
                await transaction.CommitAsync(cancellationToken);
                return result;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }
}
