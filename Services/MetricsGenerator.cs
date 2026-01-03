using Bogus;
using SseMetricsDemo.Models;

namespace SseMetricsDemo.Services;

public class MetricsGenerator
{
    private readonly Faker _faker = new();
    private long _eventId = 0;

    public SystemMetricDto Generate()
    {
        return new SystemMetricDto(
            EventId: Interlocked.Increment(ref _eventId),
            TimestampUtc: DateTime.UtcNow,
            CpuUsagePercent: _faker.Random.Double(10, 95),
            MemoryUsagePercent: _faker.Random.Double(20, 90),
            ActiveUsers: _faker.Random.Int(10, 500),
            RequestsPerSecond: _faker.Random.Int(50, 2000)
        );
    }
}