using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Services;

/// <summary>Предоставляет операции управления тренерами фитнес-клуба.</summary>
public interface ITrainerService
{
    /// <summary>Возвращает список всех сущностей.</summary>
    public Task<IReadOnlyList<TrainerDto>> GetAllAsync(CancellationToken ct = default);
    /// <summary>Возвращает сущность по идентификатору.</summary>
    public Task<TrainerDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    /// <summary>Создаёт новую сущность.</summary>
    public Task<TrainerDto> CreateAsync(UpsertTrainerDto dto, CancellationToken ct = default);
    /// <summary>Обновляет существующую сущность.</summary>
    public Task<TrainerDto?> UpdateAsync(Guid id, UpsertTrainerDto dto, CancellationToken ct = default);
    /// <summary>Удаляет сущность по идентификатору.</summary>
    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
