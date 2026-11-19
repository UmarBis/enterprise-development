using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.SqlServer.Repositories;

public sealed class BookingRepository(FitnessClubDbContext db) : IBookingRepository
{
    public async Task<IReadOnlyList<Booking>> GetAllAsync(CancellationToken ct = default)
        => await db.Bookings.AsNoTracking().ToListAsync(ct);

    public Task<Booking?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => db.Bookings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<Booking> AddAsync(Booking booking, CancellationToken ct = default)
    {
        db.Bookings.Add(booking);
        await db.SaveChangesAsync(ct);
        return booking;
    }

    public async Task<bool> UpdateAsync(Guid id, Booking booking, CancellationToken ct = default)
    {
        var current = await db.Bookings.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (current is null) return false;
        db.Entry(current).CurrentValues.SetValues(booking);
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var current = await db.Bookings.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (current is null) return false;
        db.Bookings.Remove(current);
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<IReadOnlyList<Booking>> GetByHallAndMonthAsync(
        string hallName, int year, int month, CancellationToken ct = default)
    {
        var from = new DateTime(year, month, 1);
        var to = from.AddMonths(1);
        return await db.Bookings.AsNoTracking()
            .Where(x => x.HallName == hallName && x.ScheduledAt >= from && x.ScheduledAt < to)
            .ToListAsync(ct);
    }
}
