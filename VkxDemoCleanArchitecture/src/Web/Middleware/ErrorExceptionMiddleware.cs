namespace VkxDemoCleanArchitecture.Web.Middleware;

public class ErrorExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorExceptionMiddleware> _logger;

    public ErrorExceptionMiddleware(RequestDelegate next, ILogger<ErrorExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var response = new
            {
                Message = ex.Message, 
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
