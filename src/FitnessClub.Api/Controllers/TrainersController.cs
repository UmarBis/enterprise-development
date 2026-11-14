using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>Управление тренерами.</summary>
[ApiController]
[Route("api/trainers")]
[Produces("application/json")]
public class TrainersController : ControllerBase
{
    private readonly ITrainerService _service;
    private readonly ILogger<TrainersController> _logger;

    /// <summary>Создаёт контроллер тренеров.</summary>
    /// <param name="service">Сервис тренеров.</param>
    /// <param name="logger">Журнал.</param>
    public TrainersController(ITrainerService service, ILogger<TrainersController> logger)
        => (_service, _logger) = (service, logger);

    /// <summary>Получить всех тренеров.</summary>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Успешно.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TrainerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetAll(CancellationToken ct)
    {
        _logger.LogInformation("Запрос списка тренеров");
        return Ok(await _service.GetAllAsync(ct));
    }

    /// <summary>Получить тренера по id.</summary>
    /// <param name="id">Идентификатор тренера.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Тренер найден.</response>
    /// <response code="404">Тренер не найден.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TrainerDto>> GetById(Guid id, CancellationToken ct)
    {
        _logger.LogInformation("Запрос тренера {Id}", id);
        var t = await _service.GetByIdAsync(id, ct);
        return t is null ? NotFound() : Ok(t);
    }

    /// <summary>Создать тренера.</summary>
    /// <param name="dto">Данные для создания.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="201">Тренер создан.</response>
    /// <response code="400">Некорректные данные.</response>
    [HttpPost]
    [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TrainerDto>> Create([FromBody] UpsertTrainerDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Создание тренера {LastName} {FirstName}", dto.LastName, dto.FirstName);
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>Обновить тренера.</summary>
    /// <param name="id">Идентификатор тренера.</param>
    /// <param name="dto">Новые данные.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Тренер обновлён.</response>
    /// <response code="404">Тренер не найден.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TrainerDto>> Update(Guid id, [FromBody] UpsertTrainerDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Обновление тренера {Id}", id);
        var updated = await _service.UpdateAsync(id, dto, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>Удалить тренера.</summary>
    /// <param name="id">Идентификатор тренера.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="204">Тренер удалён.</response>
    /// <response code="404">Тренер не найден.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
    {
        _logger.LogInformation("Удаление тренера {Id}", id);
        return await _service.DeleteAsync(id, ct) ? NoContent() : NotFound();
    }
}
