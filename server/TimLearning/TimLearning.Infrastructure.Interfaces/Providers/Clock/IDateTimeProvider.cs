namespace TimLearning.Infrastructure.Interfaces.Providers.Clock;

public interface IDateTimeProvider
{
    public Task<DateTimeOffset> GetUtcNow();

    public Task<DateTime> GetDateTimeUtcNow();
}
