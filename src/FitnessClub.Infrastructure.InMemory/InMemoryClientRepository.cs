using System.Collections.Concurrent;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.InMemory;

/// <summary>In-memory хранилище клиентов.</summary>
public class InMemoryClientRepository : IClientRepository
{
    private readonly ConcurrentDictionary<Guid, Client> _store = new();

    /// <summary>Создаёт хранилище, заполненное начальными данными.</summary>
    public InMemoryClientRepository(IEnumerable<Client> seed)
    {
        foreach (var client in seed)
            _store[client.Id] = client;
    }

    /// <inheritdoc/>
    public Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken ct = default)
        => Task.FromResult<IReadOnlyList<Client>>([.. _store.Values]);

    /// <inheritdoc/>
    public Task<Client?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => Task.FromResult(_store.GetValueOrDefault(id));

    /// <inheritdoc/>
    public Task<Client> AddAsync(Client client, CancellationToken ct = default)
    {
        _store[client.Id] = client;
        return Task.FromResult(client);
    }

    /// <inheritdoc/>
    public Task<bool> UpdateAsync(Guid id, Client client, CancellationToken ct = default)
    {
        if (!_store.ContainsKey(id)) return Task.FromResult(false);
        _store[id] = client;
        return Task.FromResult(true);
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => Task.FromResult(_store.TryRemove(id, out _));
}
