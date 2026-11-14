using System.ComponentModel.DataAnnotations;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>Аналитические запросы (5 из задания).</summary>
[ApiController]
[Route("api/analytics")]
[Produces("application/json")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _service;
    private readonly ILogger<AnalyticsController> _logger;

    /// <summary>Создаёт контроллер аналитики.</summary>
    /// <param name="service">Сервис аналитики.</param>
    /// <param name="logger">Журнал.</param>
    public AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger)
        => (_service, _logger) = (service, logger);

    /// <summary>Тренеры со стажем не менее заданного (по умолчанию 5 лет).</summary>
    /// <param name="minYears">Минимальный стаж в годах.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Успешно.</response>
    [HttpGet("trainers/experienced")]
    [ProducesResponseType(typeof(IReadOnlyList<TrainerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> Experienced(
        [FromQuery, Range(0, 100)] int minYears = 5, CancellationToken ct = default)
    {
        _logger.LogInformation("Аналитика: тренеры со стажем >= {MinYears}", minYears);
        return Ok(await _service.GetTrainersWithMinExperienceAsync(minYears, ct));
    }

    /// <summary>Доступность зала на указанный момент времени.</summary>
    /// <param name="hallName">Название зала.</param>
    /// <param name="at">Момент времени проверки.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Успешно.</response>
    [HttpGet("halls/{hallName}/availability")]
    [ProducesResponseType(typeof(HallAvailabilityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<HallAvailabilityDto>> HallAvailability(
        string hallName, [FromQuery] DateTime at, CancellationToken ct)
    {
        _logger.LogInformation("Аналитика: доступность зала {HallName} на {At}", hallName, at);
        return Ok(await _service.CheckHallAvailabilityAsync(hallName, at, ct));
    }

    /// <summary>Клиенты с просроченным абонементом (по ФИО).</summary>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Успешно.</response>
    [HttpGet("clients/expired")]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> ExpiredClients(CancellationToken ct)
    {
        _logger.LogInformation("Аналитика: клиенты с просроченным абонементом");
        return Ok(await _service.GetExpiredSubscriptionClientsAsync(ct));
    }

    /// <summary>Занятия в зале за месяц.</summary>
    /// <param name="hallName">Название зала.</param>
    /// <param name="year">Год.</param>
    /// <param name="month">Месяц (1–12).</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Успешно.</response>
    [HttpGet("halls/{hallName}/bookings")]
    [ProducesResponseType(typeof(IReadOnlyList<BookingDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<BookingDto>>> HallBookings(
        string hallName, [FromQuery, Range(1, 9999)] int year, [FromQuery, Range(1, 12)] int month, CancellationToken ct)
    {
        _logger.LogInformation("Аналитика: занятия в зале {HallName} за {Year}-{Month:D2}", hallName, year, month);
        return Ok(await _service.GetBookingsInHallForMonthAsync(hallName, year, month, ct));
    }

    /// <summary>Топ-N популярных тренеров (по умолчанию 5).</summary>
    /// <param name="count">Количество тренеров.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Успешно.</response>
    [HttpGet("trainers/top")]
    [ProducesResponseType(typeof(IReadOnlyList<PopularTrainerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<PopularTrainerDto>>> TopTrainers(
        [FromQuery, Range(1, 100)] int count = 5, CancellationToken ct = default)
    {
        _logger.LogInformation("Аналитика: топ-{Count} популярных тренеров", count);
        return Ok(await _service.GetTopPopularTrainersAsync(count, ct));
    }
}
