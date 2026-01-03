namespace SseMetricsDemo.Models;

public record SystemMetricDto(
    long EventId,
    DateTime TimestampUtc,
    double CpuUsagePercent,
    double MemoryUsagePercent,
    int ActiveUsers,
    int RequestsPerSecond
);
