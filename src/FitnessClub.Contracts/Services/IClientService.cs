using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Services;

/// <summary>Предоставляет операции управления клиентами фитнес-клуба.</summary>
public interface IClientService
{
    /// <summary>Возвращает список всех сущностей.</summary>
    public Task<IReadOnlyList<ClientDto>> GetAllAsync(CancellationToken ct = default);
    /// <summary>Возвращает сущность по идентификатору.</summary>
    public Task<ClientDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    /// <summary>Создаёт новую сущность.</summary>
    public Task<ClientDto> CreateAsync(UpsertClientDto dto, CancellationToken ct = default);
    /// <summary>Обновляет существующую сущность.</summary>
    public Task<ClientDto?> UpdateAsync(Guid id, UpsertClientDto dto, CancellationToken ct = default);
    /// <summary>Удаляет сущность по идентификатору.</summary>
    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
