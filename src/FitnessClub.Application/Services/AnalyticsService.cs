using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Application.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly ITrainerRepository _trainers;
    private readonly ISpecializationRepository _specs;
    private readonly IBookingRepository _bookings;
    private readonly IClientRepository _clients;
    private readonly Func<DateTime> _now;

    public AnalyticsService(ITrainerRepository trainers, ISpecializationRepository specs,
                            IBookingRepository bookings, IClientRepository clients, Func<DateTime> now)
        => (_trainers, _specs, _bookings, _clients, _now) = (trainers, specs, bookings, clients, now);

    public async Task<IReadOnlyList<TrainerDto>> GetTrainersWithMinExperienceAsync(int minYears, CancellationToken ct = default)
    {
        var specs = (await _specs.GetAllAsync(ct)).ToDictionary(s => s.Id, s => s.Name);
        return (await _trainers.GetAllAsync(ct))
            .Where(t => t.ExperienceYears >= minYears)
            .OrderByDescending(t => t.ExperienceYears)
            .Select(t => t.ToDto(specs.GetValueOrDefault(t.SpecializationId, string.Empty)))
            .ToList();
    }

    public async Task<HallAvailabilityDto> CheckHallAvailabilityAsync(string hallName, DateTime at, CancellationToken ct = default)
    {
        var busy = (await _bookings.GetAllAsync(ct)).Any(b =>
            string.Equals(b.HallName, hallName, StringComparison.OrdinalIgnoreCase)
            && b.ScheduledAt <= at && at < b.ScheduledAt.AddHours(1));
        return new HallAvailabilityDto { HallName = hallName, At = at, IsAvailable = !busy };
    }

    public async Task<IReadOnlyList<ClientDto>> GetExpiredSubscriptionClientsAsync(CancellationToken ct = default)
    {
        var today = DateOnly.FromDateTime(_now());
        return (await _clients.GetAllAsync(ct))
            .Where(c => c.IsMembershipExpired(today))
            .OrderBy(c => c.FullName)
            .Select(c => c.ToDto())
            .ToList();
    }

    public async Task<IReadOnlyList<BookingDto>> GetBookingsInHallForMonthAsync(string hallName, int year, int month, CancellationToken ct = default)
        => (await _bookings.GetByHallAndMonthAsync(hallName, year, month, ct))
            .OrderBy(b => b.ScheduledAt)
            .Select(b => b.ToDto())
            .ToList();

    public async Task<IReadOnlyList<PopularTrainerDto>> GetTopPopularTrainersAsync(int count, CancellationToken ct = default)
    {
        var trainers = await _trainers.GetAllAsync(ct);
        var result = new List<PopularTrainerDto>(trainers.Count);
        foreach (var t in trainers)
        {
            var c = await _bookings.CountByTrainerAsync(t.Id, ct);
            result.Add(new PopularTrainerDto { TrainerId = t.Id, FullName = t.FullName, BookingsCount = c });
        }
        return result.OrderByDescending(x => x.BookingsCount).Take(count).ToList();
    }
}