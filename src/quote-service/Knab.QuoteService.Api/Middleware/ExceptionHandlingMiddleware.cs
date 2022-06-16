using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace Knab.QuoteService.Api.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = context.Response;
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        
        var errorResponse = new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Detail = "Internal Server Error"
        };

        _logger.LogError("Exception: {ExceptionMessage}", exception.Message);

        var result = JsonConvert.SerializeObject(errorResponse);
        await context.Response.WriteAsync(result);
    } 
}