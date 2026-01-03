using SseMetricsDemo.Models;

namespace SseMetricsDemo.Services;

public class MetricsEventStore
{
    private readonly LinkedList<SystemMetricDto> _events = new();
    private readonly int _maxSize = 100;

    public void Add(SystemMetricDto metric)
    {
        _events.AddLast(metric);
        if (_events.Count > _maxSize)
            _events.RemoveFirst();
    }

    public IEnumerable<SystemMetricDto> GetAfter(long lastEventId)
    {
        return _events.Where(e => e.EventId > lastEventId);
    }
}
