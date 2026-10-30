using FitnessClub.Api.Infrastructure;
using FitnessClub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainersController(InMemoryRepository repo) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Trainer>> GetAll() => Ok(repo.GetTrainers());

    [HttpPost]
    public IActionResult Create([FromBody] Trainer trainer)
    {
        repo.AddTrainer(trainer);
        return Ok(trainer);
    }
}