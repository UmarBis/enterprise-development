using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

/// <summary>Определяет операции хранения тренеров.</summary>
public interface ITrainerRepository
{
    /// <summary>Возвращает всех тренеров.</summary>
    Task<IReadOnlyList<Trainer>> GetAllAsync(CancellationToken ct = default);
    /// <summary>Возвращает тренера по идентификатору.</summary>
    Task<Trainer?> GetByIdAsync(Guid id, CancellationToken ct = default);
    /// <summary>Добавляет тренера.</summary>
    Task<Trainer> AddAsync(Trainer trainer, CancellationToken ct = default);
    /// <summary>Обновляет тренера.</summary>
    Task<bool> UpdateAsync(Guid id, Trainer trainer, CancellationToken ct = default);
    /// <summary>Удаляет тренера.</summary>
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
