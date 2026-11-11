using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Application.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookings;
    private readonly Func<DateTime> _now;

    public BookingService(IBookingRepository bookings, Func<DateTime> now)
        => (_bookings, _now) = (bookings, now);

    public async Task<IReadOnlyList<BookingDto>> GetAllAsync(CancellationToken ct = default)
        => (await _bookings.GetAllAsync(ct)).Select(b => b.ToDto()).ToList();

    public async Task<BookingDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => (await _bookings.GetByIdAsync(id, ct))?.ToDto();

    public async Task<BookingDto> CreateAsync(UpsertBookingDto dto, CancellationToken ct = default)
    {
        var entity = new Booking
        {
            ClientId = dto.ClientId!.Value,
            TrainerId = dto.TrainerId!.Value,
            HallName = dto.HallName,
            ScheduledAt = dto.SessionDateTime!.Value,
            IsTrial = dto.IsTrial
        };
        return (await _bookings.AddAsync(entity, ct)).ToDto();
    }

    public async Task<BookingDto?> UpdateAsync(Guid id, UpsertBookingDto dto, CancellationToken ct = default)
    {
        var exists = await _bookings.GetByIdAsync(id, ct);
        if (exists is null) return null;
        var updated = new Booking
        {
            Id = id,
            ClientId = dto.ClientId!.Value,
            TrainerId = dto.TrainerId!.Value,
            HallName = dto.HallName,
            ScheduledAt = dto.SessionDateTime!.Value,
            IsTrial = dto.IsTrial
        };
        await _bookings.UpdateAsync(id, updated, ct);
        return updated.ToDto();
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => _bookings.DeleteAsync(id, ct);

    public async Task<IReadOnlyList<BookingDto>> GetBookingsForCurrentMonthAsync(string hallName, CancellationToken ct = default)
    {
        var now = _now();
        return (await _bookings.GetByHallAndMonthAsync(hallName, now.Year, now.Month, ct))
            .Select(b => b.ToDto())
            .ToList();
    }
}