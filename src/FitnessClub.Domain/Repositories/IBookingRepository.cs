using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

/// <summary>Определяет операции хранения и выборки записей на занятия.</summary>
public interface IBookingRepository
{
    /// <summary>Возвращает все записи.</summary>
    public Task<IReadOnlyList<Booking>> GetAllAsync(CancellationToken ct = default);
    /// <summary>Возвращает запись по идентификатору.</summary>
    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken ct = default);
    /// <summary>Добавляет запись.</summary>
    public Task<Booking> AddAsync(Booking booking, CancellationToken ct = default);
    /// <summary>Обновляет запись.</summary>
    public Task<bool> UpdateAsync(Guid id, Booking booking, CancellationToken ct = default);
    /// <summary>Удаляет запись.</summary>
    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    /// <summary>Возвращает записи выбранного зала за указанный месяц.</summary>
    public Task<IReadOnlyList<Booking>> GetByHallAndMonthAsync(string hallName, int year, int month, CancellationToken ct = default);
}
