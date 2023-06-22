using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights;

namespace MeuMenu.Api.Middlewares;

public class RequestTelemetryMiddleware
{
    private readonly RequestDelegate _next;
    private readonly TelemetryClient _telemetryClient;
    private readonly ILogger<RequestTelemetryMiddleware> _logger;

    public RequestTelemetryMiddleware(RequestDelegate next, TelemetryClient telemetryClient, ILogger<RequestTelemetryMiddleware> logger)
    {
        _next = next;
        _telemetryClient = telemetryClient;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.ContentLength > 0)
        {
            context.Request.EnableBuffering();
            using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
            {
                var requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                var requestTelemetry = context.Features.Get<RequestTelemetry>();
                requestTelemetry?.Properties.Add("RequestBody", requestBody);
            }
        }

        await _next(context);
    }
}