using System.Collections.Concurrent;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.InMemory;

public class InMemoryTrainerRepository : ITrainerRepository
{
    private readonly ConcurrentDictionary<Guid, Trainer> _store = new();

    public InMemoryTrainerRepository() { }
    public InMemoryTrainerRepository(IEnumerable<Trainer> seed)
    {
        foreach (var t in seed) _store[t.Id] = t;
    }

    public Task<IReadOnlyList<Trainer>> GetAllAsync(CancellationToken ct = default)
        => Task.FromResult<IReadOnlyList<Trainer>>([.. _store.Values]);

    public Task<Trainer?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => Task.FromResult(_store.TryGetValue(id, out var t) ? t : null);

    public Task<Trainer> AddAsync(Trainer trainer, CancellationToken ct = default)
    {
        _store[trainer.Id] = trainer;
        return Task.FromResult(trainer);
    }

    public Task<bool> UpdateAsync(Guid id, Trainer trainer, CancellationToken ct = default)
    {
        if (!_store.ContainsKey(id)) return Task.FromResult(false);
        _store[id] = trainer;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => Task.FromResult(_store.TryRemove(id, out _));
}