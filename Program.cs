using SseMetricsDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<MetricsGenerator>();
builder.Services.AddSingleton<MetricsEventStore>();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
