using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

/// <summary>Определяет операции хранения клиентов.</summary>
public interface IClientRepository
{
    /// <summary>Возвращает всех клиентов.</summary>
    Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken ct = default);
    /// <summary>Возвращает клиента по идентификатору.</summary>
    Task<Client?> GetByIdAsync(Guid id, CancellationToken ct = default);
    /// <summary>Добавляет клиента.</summary>
    Task<Client> AddAsync(Client client, CancellationToken ct = default);
    /// <summary>Обновляет клиента.</summary>
    Task<bool> UpdateAsync(Guid id, Client client, CancellationToken ct = default);
    /// <summary>Удаляет клиента.</summary>
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
