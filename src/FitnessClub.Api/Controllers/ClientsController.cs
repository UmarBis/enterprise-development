using FitnessClub.Api.Infrastructure;
using FitnessClub.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(InMemoryRepository repo) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Client>> GetAll() => Ok(repo.GetClients());

    [HttpGet("{id:guid}")]
    public ActionResult<Client> GetById(Guid id)
    {
        var client = repo.GetClient(id);
        return client == null ? NotFound() : Ok(client);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Client client)
    {
        repo.AddClient(client);
        return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        return repo.DeleteClient(id) ? NoContent() : NotFound();
    }
}
