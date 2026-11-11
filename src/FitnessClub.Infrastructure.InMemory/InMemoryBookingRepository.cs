using System.Collections.Concurrent;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.InMemory;

public class InMemoryBookingRepository : IBookingRepository
{
    private readonly ConcurrentDictionary<Guid, Booking> _store = new();

    public InMemoryBookingRepository() { }
    public InMemoryBookingRepository(IEnumerable<Booking> seed)
    {
        foreach (var b in seed) _store[b.Id] = b;
    }

    public Task<IReadOnlyList<Booking>> GetAllAsync(CancellationToken ct = default)
        => Task.FromResult<IReadOnlyList<Booking>>(_store.Values.ToList());

    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => Task.FromResult(_store.TryGetValue(id, out var b) ? b : null);

    public Task<Booking> AddAsync(Booking booking, CancellationToken ct = default)
    {
        _store[booking.Id] = booking;
        return Task.FromResult(booking);
    }

    public Task<bool> UpdateAsync(Guid id, Booking booking, CancellationToken ct = default)
    {
        if (!_store.ContainsKey(id)) return Task.FromResult(false);
        _store[id] = booking;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => Task.FromResult(_store.TryRemove(id, out _));

    public Task<bool> HasByTrainerAsync(Guid trainerId, CancellationToken ct = default)
        => Task.FromResult(_store.Values.Any(b => b.TrainerId == trainerId));

    public Task<IReadOnlyList<Booking>> GetByHallAndMonthAsync(string hallName, int year, int month, CancellationToken ct = default)
        => Task.FromResult<IReadOnlyList<Booking>>(_store.Values
            .Where(b => string.Equals(b.HallName, hallName, StringComparison.OrdinalIgnoreCase)
                        && b.ScheduledAt.Year == year
                        && b.ScheduledAt.Month == month)
            .ToList());

    public Task<int> CountByTrainerAsync(Guid trainerId, CancellationToken ct = default)
        => Task.FromResult(_store.Values.Count(b => b.TrainerId == trainerId));
}