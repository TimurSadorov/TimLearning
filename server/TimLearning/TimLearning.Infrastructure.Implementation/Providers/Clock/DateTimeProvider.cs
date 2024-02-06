using TimLearning.Infrastructure.Interfaces.Providers.Clock;

namespace TimLearning.Infrastructure.Implementation.Providers.Clock;

public class DateTimeProvider : IDateTimeProvider
{
    public Task<DateTimeOffset> GetUtcNow()
    {
        return Task.FromResult(DateTimeOffset.UtcNow);
    }

    public async Task<DateTime> GetDateTimeUtcNow()
    {
        return (await GetUtcNow()).UtcDateTime;
    }
}
