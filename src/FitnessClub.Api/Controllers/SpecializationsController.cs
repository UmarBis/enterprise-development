using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>Справочник специализаций.</summary>
[ApiController]
[Route("api/specializations")]
[Produces("application/json")]
public class SpecializationsController : ControllerBase
{
    private readonly ISpecializationService _service;
    private readonly ILogger<SpecializationsController> _logger;

    /// <summary>Создаёт контроллер специализаций.</summary>
    /// <param name="service">Сервис специализаций.</param>
    /// <param name="logger">Журнал.</param>
    public SpecializationsController(ISpecializationService service, ILogger<SpecializationsController> logger)
        => (_service, _logger) = (service, logger);

    /// <summary>Получить все специализации.</summary>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Успешно.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<SpecializationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<SpecializationDto>>> GetAll(CancellationToken ct)
    {
        _logger.LogInformation("Запрос списка специализаций");
        return Ok(await _service.GetAllAsync(ct));
    }

    /// <summary>Получить специализацию по id.</summary>
    /// <param name="id">Идентификатор специализации.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Специализация найдена.</response>
    /// <response code="404">Специализация не найдена.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> GetById(Guid id, CancellationToken ct)
    {
        _logger.LogInformation("Запрос специализации {Id}", id);
        var s = await _service.GetByIdAsync(id, ct);
        return s is null ? NotFound() : Ok(s);
    }

    /// <summary>Создать специализацию.</summary>
    /// <param name="dto">Данные для создания.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="201">Специализация создана.</response>
    /// <response code="400">Некорректные данные.</response>
    [HttpPost]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> Create([FromBody] UpsertSpecializationDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Создание специализации {Name}", dto.Name);
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>Обновить специализацию.</summary>
    /// <param name="id">Идентификатор специализации.</param>
    /// <param name="dto">Новые данные.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Специализация обновлена.</response>
    /// <response code="404">Специализация не найдена.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> Update(Guid id, [FromBody] UpsertSpecializationDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Обновление специализации {Id}", id);
        var updated = await _service.UpdateAsync(id, dto, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>Удалить специализацию.</summary>
    /// <param name="id">Идентификатор специализации.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="204">Специализация удалена.</response>
    /// <response code="404">Специализация не найдена.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
    {
        _logger.LogInformation("Удаление специализации {Id}", id);
        return await _service.DeleteAsync(id, ct) ? NoContent() : NotFound();
    }
}
