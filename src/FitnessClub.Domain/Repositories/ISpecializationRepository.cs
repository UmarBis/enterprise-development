using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

/// <summary>Определяет операции хранения специализаций.</summary>
public interface ISpecializationRepository
{
    /// <summary>Возвращает все специализации.</summary>
    Task<IReadOnlyList<Specialization>> GetAllAsync(CancellationToken ct = default);
    /// <summary>Возвращает специализацию по идентификатору.</summary>
    Task<Specialization?> GetByIdAsync(Guid id, CancellationToken ct = default);
    /// <summary>Добавляет специализацию.</summary>
    Task<Specialization> AddAsync(Specialization specialization, CancellationToken ct = default);
    /// <summary>Обновляет специализацию.</summary>
    Task<bool> UpdateAsync(Guid id, Specialization specialization, CancellationToken ct = default);
    /// <summary>Удаляет специализацию.</summary>
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
