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
    public AnalyticsController(IAnalyticsService service) => _service = service;

    /// <summary>Тренеры со стажем не менее заданного (по умолчанию 5 лет).</summary>
    [HttpGet("trainers/experienced")]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> Experienced(
        [FromQuery] int minYears = 5, CancellationToken ct = default)
        => Ok(await _service.GetTrainersWithMinExperienceAsync(minYears, ct));

    /// <summary>Доступность зала на указанный момент времени.</summary>
    [HttpGet("halls/{hallName}/availability")]
    public async Task<ActionResult<HallAvailabilityDto>> HallAvailability(
        string hallName, [FromQuery] DateTime at, CancellationToken ct)
        => Ok(await _service.CheckHallAvailabilityAsync(hallName, at, ct));

    /// <summary>Клиенты с просроченным абонементом (по ФИО).</summary>
    [HttpGet("clients/expired")]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> ExpiredClients(CancellationToken ct)
        => Ok(await _service.GetExpiredSubscriptionClientsAsync(ct));

    /// <summary>Занятия в зале за месяц.</summary>
    [HttpGet("halls/{hallName}/bookings")]
    public async Task<ActionResult<IReadOnlyList<BookingDto>>> HallBookings(
        string hallName, [FromQuery] int year, [FromQuery] int month, CancellationToken ct)
        => Ok(await _service.GetBookingsInHallForMonthAsync(hallName, year, month, ct));

    /// <summary>Топ-N популярных тренеров (по умолчанию 5).</summary>
    [HttpGet("trainers/top")]
    public async Task<ActionResult<IReadOnlyList<PopularTrainerDto>>> TopTrainers(
        [FromQuery] int count = 5, CancellationToken ct = default)
        => Ok(await _service.GetTopPopularTrainersAsync(count, ct));
}