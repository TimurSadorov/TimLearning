namespace TimLearning.Infrastructure.Interfaces.Providers.Clock;

public interface IDateTimeProvider
{
    public ValueTask<DateTimeOffset> GetUtcNow();

    public ValueTask<DateTime> GetDateTimeUtcNow();
}
