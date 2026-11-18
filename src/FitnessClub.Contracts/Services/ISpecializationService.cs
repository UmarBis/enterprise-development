using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Services;

/// <summary>Предоставляет операции управления специализациями тренеров.</summary>
public interface ISpecializationService
{
    /// <summary>Возвращает список всех сущностей.</summary>
    public Task<IReadOnlyList<SpecializationDto>> GetAllAsync(CancellationToken ct = default);
    /// <summary>Возвращает сущность по идентификатору.</summary>
    public Task<SpecializationDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    /// <summary>Создаёт новую сущность.</summary>
    public Task<SpecializationDto> CreateAsync(UpsertSpecializationDto dto, CancellationToken ct = default);
    /// <summary>Обновляет существующую сущность.</summary>
    public Task<SpecializationDto?> UpdateAsync(Guid id, UpsertSpecializationDto dto, CancellationToken ct = default);
    /// <summary>Удаляет сущность по идентификатору.</summary>
    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
