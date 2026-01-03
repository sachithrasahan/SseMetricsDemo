using Microsoft.AspNetCore.Mvc;
using SseMetricsDemo.Services;
using System.Data;
using System.Text.Json;

namespace SseMetricsDemo.Controllers;

[ApiController]
[Route("api/v1/stream")]
public class MetricsStreamController : ControllerBase
{
    private readonly MetricsGenerator _generator;
    private readonly MetricsEventStore _store;

    public MetricsStreamController(
        MetricsGenerator generator,
        MetricsEventStore store)
    {
        _generator = generator;
        _store = store;
    }

    [HttpGet("system-metrics")]
    public async Task Stream(CancellationToken ct)
    {
        Response.Headers.Append("Content-Type", "text/event-stream");
        Response.Headers.Append("Cache-Control", "no-cache");
        Response.Headers.Append("X-Accel-Buffering", "no");

        // Get last event ID from client
        var lastEventIdHeader = Request.Headers["Last-Event-ID"].FirstOrDefault();
        long lastEventId = 0;
        long.TryParse(lastEventIdHeader, out lastEventId);

        // Send missed events (replay)
        foreach (var evt in _store.GetAfter(lastEventId))
        {
            await SendEvent(evt, ct);
        }

        var connectionStart = DateTime.UtcNow;

        // Live stream
        while (!ct.IsCancellationRequested)
        {
            var metric = _generator.Generate();
            _store.Add(metric);

            await SendEvent(metric, ct);
            await Task.Delay(2000, ct);
        }
    }

    private async Task SendEvent(object data, CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(data);
        var id = data.GetType().GetProperty("EventId")?.GetValue(data);

        await Response.WriteAsync($"id: {id}\n", ct);
        await Response.WriteAsync($"event: system-metric\n", ct);
        await Response.WriteAsync($"data: {json}\n\n", ct);
        await Response.Body.FlushAsync(ct);
    }
}
