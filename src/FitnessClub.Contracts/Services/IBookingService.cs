using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Services;

/// <summary>Предоставляет операции управления записями на занятия.</summary>
public interface IBookingService
{
    /// <summary>Возвращает список всех сущностей.</summary>
    public Task<IReadOnlyList<BookingDto>> GetAllAsync(CancellationToken ct = default);
    /// <summary>Возвращает сущность по идентификатору.</summary>
    public Task<BookingDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    /// <summary>Создаёт новую сущность.</summary>
    public Task<BookingDto> CreateAsync(UpsertBookingDto dto, CancellationToken ct = default);
    /// <summary>Обновляет существующую сущность.</summary>
    public Task<BookingDto?> UpdateAsync(Guid id, UpsertBookingDto dto, CancellationToken ct = default);
    /// <summary>Удаляет сущность по идентификатору.</summary>
    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
