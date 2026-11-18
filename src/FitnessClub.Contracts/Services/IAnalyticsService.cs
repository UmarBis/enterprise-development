using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Services;

/// <summary>Предоставляет аналитические операции фитнес-клуба.</summary>
public interface IAnalyticsService
{
    /// <summary>Возвращает тренеров с заданным минимальным стажем.</summary>
    public Task<IReadOnlyList<TrainerDto>> GetTrainersWithMinExperienceAsync(int minYears, CancellationToken ct = default);
    /// <summary>Проверяет доступность зала в указанное время.</summary>
    public Task<HallAvailabilityDto> CheckHallAvailabilityAsync(string hallName, DateTime at, CancellationToken ct = default);
    /// <summary>Возвращает клиентов с истёкшим абонементом.</summary>
    public Task<IReadOnlyList<ClientDto>> GetExpiredSubscriptionClientsAsync(CancellationToken ct = default);
    /// <summary>Возвращает записи в указанном зале за месяц.</summary>
    public Task<IReadOnlyList<BookingDto>> GetBookingsInHallForMonthAsync(string hallName, int year, int month, CancellationToken ct = default);
    /// <summary>Возвращает наиболее популярных тренеров.</summary>
    public Task<IReadOnlyList<PopularTrainerDto>> GetTopPopularTrainersAsync(int count, CancellationToken ct = default);
}
