using Microsoft.AspNetCore.Http;

namespace BattleshipGame.Infrastructure.RequestsContext;

public class RequestContextMiddleware
{
    private readonly RequestDelegate _next;

    public RequestContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, RequestContextBundle requestContext)
    {
        // Capture custom header (e.g., X-Client-Application)
        requestContext.ClientApplication = context.Request.Headers["X-Client-Application"].FirstOrDefault();

        // Capture IP address
        requestContext.IpAddress = context.Connection.RemoteIpAddress?.ToString();
        
        if (string.IsNullOrEmpty(requestContext.ClientApplication))
        {
            // Capture User-Agent (useful for identifying Postman)
            // Optionally allow local debugging or Postman
            var userAgent = context.Request.Headers["User-Agent"].FirstOrDefault();
            if (userAgent?.Contains("Postman", StringComparison.OrdinalIgnoreCase) == true)
            {
                requestContext.ClientApplication = "Postman";
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Missing X-Client-Application header");
                return;
            }
        }
        
        requestContext.IdempotencyKey = context.Request.Headers["X-Idempotency-Key"].FirstOrDefault();
        requestContext.CorrelationKey = context.Request.Headers["X-Correlation-Key"].FirstOrDefault();
        requestContext.SagaProcessKey = context.Request.Headers["X-Saga-Process-Key"].FirstOrDefault();

        await _next(context);
    }
}