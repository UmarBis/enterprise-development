using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

/// <summary>Определяет операции хранения тренеров.</summary>
public interface ITrainerRepository
{
    /// <summary>Возвращает всех тренеров.</summary>
    public Task<IReadOnlyList<Trainer>> GetAllAsync(CancellationToken ct = default);
    /// <summary>Возвращает тренера по идентификатору.</summary>
    public Task<Trainer?> GetByIdAsync(Guid id, CancellationToken ct = default);
    /// <summary>Добавляет тренера.</summary>
    public Task<Trainer> AddAsync(Trainer trainer, CancellationToken ct = default);
    /// <summary>Обновляет тренера.</summary>
    public Task<bool> UpdateAsync(Guid id, Trainer trainer, CancellationToken ct = default);
    /// <summary>Удаляет тренера.</summary>
    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
