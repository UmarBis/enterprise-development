using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

public interface IBookingRepository
{
    Task<IReadOnlyList<Booking>> GetAllAsync(CancellationToken ct = default);
    Task<Booking?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Booking> AddAsync(Booking booking, CancellationToken ct = default);
    Task<bool> UpdateAsync(Guid id, Booking booking, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    Task<bool> HasByTrainerAsync(Guid trainerId, CancellationToken ct = default);
    Task<IReadOnlyList<Booking>> GetByHallAndMonthAsync(string hallName, int year, int month, CancellationToken ct = default);
    Task<int> CountByTrainerAsync(Guid trainerId, CancellationToken ct = default);
}