using FitnessClub.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

[ApiController]
[Route("api/analytics")]
public class AnalyticsController(AnalyticsService analytics) : ControllerBase
{
    [HttpGet("experienced-trainers")]
    public IActionResult GetExperiencedTrainers() => Ok(analytics.GetTrainersWith5YearsExperience());

    [HttpGet("expired-clients")]
    public IActionResult GetExpiredClients() => Ok(analytics.GetExpiredClients());

    [HttpGet("popular-trainers")]
    public IActionResult GetPopularTrainers() => Ok(analytics.GetTop5Trainers());

    [HttpGet("hall-availability")]
    public IActionResult CheckHall([FromQuery] string hallName, [FromQuery] DateTime at)
        => Ok(new { HallName = hallName, Available = analytics.IsHallAvailable(hallName, at, 60) });

    [HttpGet("current-month-bookings")]
    public IActionResult GetCurrentMonthBookings([FromQuery] string hallName)
        => Ok(analytics.GetCurrentMonthBookings(hallName));
}
