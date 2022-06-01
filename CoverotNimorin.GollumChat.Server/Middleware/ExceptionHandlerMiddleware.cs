using System.Net;
using System.Text.Json;
using CoverotNimorin.GollumChat.Server.Payloads;
using WebException = CoverotNimorin.GollumChat.Server.Exceptions.WebException;

namespace CoverotNimorin.GollumChat.Server.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlerMiddleware> logger
    )
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
        catch (Exception exception)
        {
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            ResultPayload responseModel = new ResultPayload(exception.Message);

            string? targetClassName = 
                exception.TargetSite?.ReflectedType?.FullName 
                ?? exception.TargetSite?.ReflectedType?.Name;
            
            switch (exception)
            {
                case WebException webException:
                    response.StatusCode = (int)webException.Status;
                    _logger.LogInformation(
                        "{0} - {1}",
                        targetClassName,
                        string.Join("\n#####\n", webException.Messages)
                    );
                    
                    break;

                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    _logger.LogInformation(
                        "{0} - {1}",
                        targetClassName,
                        exception.Message
                    );
                    
                    break;
            }

            string result = JsonSerializer.Serialize(responseModel);
            await response.WriteAsync(result);
        }
    }
}