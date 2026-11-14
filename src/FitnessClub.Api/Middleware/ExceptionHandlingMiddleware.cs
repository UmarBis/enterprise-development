using FitnessClub.Application.Exceptions;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Middleware;

/// <summary>Преобразует исключения приложения в стандартизированные ProblemDetails.</summary>
public sealed class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    /// <summary>Обрабатывает HTTP-запрос и перехватывает ожидаемые исключения приложения.</summary>
    /// <param name="context">Контекст HTTP-запроса.</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
        {
            // Клиент отключился: формировать HTTP-ответ уже некому.
        }
        catch (Exception exception)
        {
            var (statusCode, title) = exception switch
            {
                EntityNotFoundException => (StatusCodes.Status404NotFound, "Ресурс не найден"),
                ConflictException => (StatusCodes.Status409Conflict, "Конфликт данных"),
                ValidationException or ArgumentException => (StatusCodes.Status400BadRequest, "Некорректный запрос"),
                _ => (StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера")
            };

            if (statusCode >= StatusCodes.Status500InternalServerError)
            {
                logger.LogError(exception,
                    "Ошибка при обработке {Method} {Path}. TraceId={TraceId}",
                    context.Request.Method, context.Request.Path, context.TraceIdentifier);
            }
            else
            {
                logger.LogWarning(exception,
                    "Запрос {Method} {Path} завершился с HTTP {StatusCode}. TraceId={TraceId}",
                    context.Request.Method, context.Request.Path, statusCode, context.TraceIdentifier);
            }

            if (context.Response.HasStarted)
                throw;

            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = statusCode >= StatusCodes.Status500InternalServerError
                    ? "Произошла непредвиденная ошибка. Повторите запрос позже."
                    : exception.Message,
                Instance = context.Request.Path
            };
            problem.Extensions["traceId"] = context.TraceIdentifier;

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
