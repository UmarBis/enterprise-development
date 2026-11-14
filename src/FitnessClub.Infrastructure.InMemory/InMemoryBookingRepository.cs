using System.Collections.Concurrent;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Infrastructure.InMemory;

/// <summary>In-memory хранилище записей на занятия.</summary>
public class InMemoryBookingRepository : IBookingRepository
{
    private readonly ConcurrentDictionary<Guid, Booking> _store = new();

    /// <summary>Создаёт хранилище, заполненное начальными данными.</summary>
    public InMemoryBookingRepository(IEnumerable<Booking> seed)
    {
        foreach (var booking in seed)
            _store[booking.Id] = booking;
    }

    /// <inheritdoc/>
    public Task<IReadOnlyList<Booking>> GetAllAsync(CancellationToken ct = default)
        => Task.FromResult<IReadOnlyList<Booking>>([.. _store.Values]);

    /// <inheritdoc/>
    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => Task.FromResult(_store.GetValueOrDefault(id));

    /// <inheritdoc/>
    public Task<Booking> AddAsync(Booking booking, CancellationToken ct = default)
    {
        _store[booking.Id] = booking;
        return Task.FromResult(booking);
    }

    /// <inheritdoc/>
    public Task<bool> UpdateAsync(Guid id, Booking booking, CancellationToken ct = default)
    {
        if (!_store.ContainsKey(id)) return Task.FromResult(false);
        _store[id] = booking;
        return Task.FromResult(true);
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => Task.FromResult(_store.TryRemove(id, out _));

    /// <inheritdoc/>
    public Task<IReadOnlyList<Booking>> GetByHallAndMonthAsync(
        string hallName, int year, int month, CancellationToken ct = default)
        => Task.FromResult<IReadOnlyList<Booking>>([.. _store.Values.Where(booking =>
            string.Equals(booking.HallName, hallName, StringComparison.OrdinalIgnoreCase)
            && booking.ScheduledAt.Year == year
            && booking.ScheduledAt.Month == month)]);

}
