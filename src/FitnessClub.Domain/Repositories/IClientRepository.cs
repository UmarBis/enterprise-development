using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

public interface IClientRepository
{
    Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken ct = default);
    Task<Client?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Client> AddAsync(Client client, CancellationToken ct = default);
    Task<bool> UpdateAsync(Guid id, Client client, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    Task<bool> HasByClientAsync(Guid clientId, CancellationToken ct = default);
}