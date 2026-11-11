using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Services;

public interface IAnalyticsService
{
    Task<IReadOnlyList<TrainerDto>> GetTrainersWithMinExperienceAsync(int minYears, CancellationToken ct = default);
    Task<HallAvailabilityDto> CheckHallAvailabilityAsync(string hallName, DateTime at, CancellationToken ct = default);
    Task<IReadOnlyList<ClientDto>> GetExpiredSubscriptionClientsAsync(CancellationToken ct = default);
    Task<IReadOnlyList<BookingDto>> GetBookingsInHallForMonthAsync(string hallName, int year, int month, CancellationToken ct = default);
    Task<IReadOnlyList<PopularTrainerDto>> GetTopPopularTrainersAsync(int count, CancellationToken ct = default);
}