using System.Collections.Concurrent;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.InMemory;

public class InMemorySpecializationRepository : ISpecializationRepository
{
    private readonly ConcurrentDictionary<Guid, Specialization> _store = new();

    public InMemorySpecializationRepository() { }
    public InMemorySpecializationRepository(IEnumerable<Specialization> seed)
    {
        foreach (var s in seed) _store[s.Id] = s;
    }

    public Task<IReadOnlyList<Specialization>> GetAllAsync(CancellationToken ct = default)
        => Task.FromResult<IReadOnlyList<Specialization>>(_store.Values.ToList());

    public Task<Specialization?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => Task.FromResult(_store.TryGetValue(id, out var s) ? s : null);

    public Task<Specialization> AddAsync(Specialization specialization, CancellationToken ct = default)
    {
        _store[specialization.Id] = specialization;
        return Task.FromResult(specialization);
    }

    public Task<bool> UpdateAsync(Guid id, Specialization specialization, CancellationToken ct = default)
    {
        if (!_store.ContainsKey(id)) return Task.FromResult(false);
        _store[id] = specialization;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => Task.FromResult(_store.TryRemove(id, out _));
}