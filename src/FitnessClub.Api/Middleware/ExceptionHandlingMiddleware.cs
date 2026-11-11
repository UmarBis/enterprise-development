using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Middleware;

/// <summary>Глобальный обработчик исключений: логирует и возвращает ProblemDetails.</summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    /// <summary>Создаёт middleware.</summary>
    /// <param name="next">Следующий делегат в конвейере.</param>
    /// <param name="logger">Журнал.</param>
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        => (_next, _logger) = (next, logger);

    /// <summary>Обрабатывает запрос, перехватывая исключения.</summary>
    /// <param name="context">Контекст HTTP-запроса.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var traceId = context.TraceIdentifier;
            _logger.LogError(ex, "Необработанная ошибка при {Method} {Path}. TraceId={TraceId}",
                context.Request.Method, context.Request.Path, traceId);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/problem+json";

            var problem = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Title = "Внутренняя ошибка сервера"
            };
            
            // TraceId кладется в Extensions
            problem.Extensions["traceId"] = traceId;

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}