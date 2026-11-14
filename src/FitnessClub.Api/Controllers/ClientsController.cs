using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>Управление клиентами.</summary>
[ApiController]
[Route("api/clients")]
[Produces("application/json")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _service;
    private readonly ILogger<ClientsController> _logger;

    /// <summary>Создаёт контроллер клиентов.</summary>
    /// <param name="service">Сервис клиентов.</param>
    /// <param name="logger">Журнал.</param>
    public ClientsController(IClientService service, ILogger<ClientsController> logger)
        => (_service, _logger) = (service, logger);

    /// <summary>Получить всех клиентов.</summary>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Успешно.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetAll(CancellationToken ct)
    {
        _logger.LogInformation("Запрос списка клиентов");
        return Ok(await _service.GetAllAsync(ct));
    }

    /// <summary>Получить клиента по id.</summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Клиент найден.</response>
    /// <response code="404">Клиент не найден.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientDto>> GetById(Guid id, CancellationToken ct)
    {
        _logger.LogInformation("Запрос клиента {Id}", id);
        var c = await _service.GetByIdAsync(id, ct);
        return c is null ? NotFound() : Ok(c);
    }

    /// <summary>Создать клиента.</summary>
    /// <param name="dto">Данные для создания.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="201">Клиент создан.</response>
    /// <response code="400">Некорректные данные.</response>
    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientDto>> Create([FromBody] UpsertClientDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Создание клиента {LastName} {FirstName}", dto.LastName, dto.FirstName);
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>Обновить клиента.</summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <param name="dto">Новые данные.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Клиент обновлён.</response>
    /// <response code="404">Клиент не найден.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientDto>> Update(Guid id, [FromBody] UpsertClientDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Обновление клиента {Id}", id);
        var updated = await _service.UpdateAsync(id, dto, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>Удалить клиента.</summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="204">Клиент удалён.</response>
    /// <response code="404">Клиент не найден.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
    {
        _logger.LogInformation("Удаление клиента {Id}", id);
        return await _service.DeleteAsync(id, ct) ? NoContent() : NotFound();
    }
}
