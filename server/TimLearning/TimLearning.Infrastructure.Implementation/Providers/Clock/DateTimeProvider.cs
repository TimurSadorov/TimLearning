using TimLearning.Infrastructure.Interfaces.Providers.Clock;

namespace TimLearning.Infrastructure.Implementation.Providers.Clock;

public class DateTimeProvider : IDateTimeProvider
{
    public ValueTask<DateTimeOffset> GetUtcNow()
    {
        return ValueTask.FromResult(DateTimeOffset.UtcNow);
    }

    public async ValueTask<DateTime> GetDateTimeUtcNow()
    {
        return (await GetUtcNow()).UtcDateTime;
    }
}
