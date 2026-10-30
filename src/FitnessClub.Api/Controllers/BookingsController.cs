using FitnessClub.Api.Infrastructure;
using FitnessClub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController(InMemoryRepository repo) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Booking>> GetAll() => Ok(repo.GetBookings());

    [HttpPost]
    public IActionResult Create([FromBody] Booking booking)
    {
        repo.AddBooking(booking);
        return Ok(booking);
    }
}