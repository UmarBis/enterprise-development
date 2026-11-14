using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>Управление записями на занятия.</summary>
[ApiController]
[Route("api/bookings")]
[Produces("application/json")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _service;
    private readonly ILogger<BookingsController> _logger;

    /// <summary>Создаёт контроллер записей.</summary>
    /// <param name="service">Сервис записей.</param>
    /// <param name="logger">Журнал.</param>
    public BookingsController(IBookingService service, ILogger<BookingsController> logger)
        => (_service, _logger) = (service, logger);

    /// <summary>Получить все записи.</summary>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Успешно.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<BookingDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<BookingDto>>> GetAll(CancellationToken ct)
    {
        _logger.LogInformation("Запрос списка записей");
        return Ok(await _service.GetAllAsync(ct));
    }

    /// <summary>Получить запись по id.</summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Запись найдена.</response>
    /// <response code="404">Запись не найдена.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BookingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BookingDto>> GetById(Guid id, CancellationToken ct)
    {
        _logger.LogInformation("Запрос записи {Id}", id);
        var b = await _service.GetByIdAsync(id, ct);
        return b is null ? NotFound() : Ok(b);
    }

    /// <summary>Создать запись на занятие.</summary>
    /// <param name="dto">Данные для создания.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="201">Запись создана.</response>
    /// <response code="400">Некорректные данные.</response>
    [HttpPost]
    [ProducesResponseType(typeof(BookingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BookingDto>> Create([FromBody] UpsertBookingDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Создание записи в зале {HallName}", dto.HallName);
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>Обновить запись.</summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <param name="dto">Новые данные.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="200">Запись обновлена.</response>
    /// <response code="404">Запись не найдена.</response>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(BookingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BookingDto>> Update(Guid id, [FromBody] UpsertBookingDto dto, CancellationToken ct)
    {
        _logger.LogInformation("Обновление записи {Id}", id);
        var updated = await _service.UpdateAsync(id, dto, ct);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>Удалить запись.</summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <param name="ct">Токен отмены.</param>
    /// <response code="204">Запись удалена.</response>
    /// <response code="404">Запись не найдена.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
    {
        _logger.LogInformation("Удаление записи {Id}", id);
        return await _service.DeleteAsync(id, ct) ? NoContent() : NotFound();
    }
}
