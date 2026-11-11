using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Services;

public interface IBookingService
{
    Task<IReadOnlyList<BookingDto>> GetAllAsync(CancellationToken ct = default);
    Task<BookingDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<BookingDto> CreateAsync(UpsertBookingDto dto, CancellationToken ct = default);
    Task<BookingDto?> UpdateAsync(Guid id, UpsertBookingDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}