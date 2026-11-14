using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Application.Services;

/// <summary>Реализует аналитические запросы фитнес-клуба.</summary>
public class AnalyticsService(
    ITrainerRepository trainers,
    ISpecializationRepository specs,
    IBookingRepository bookings,
    IClientRepository clients,
    TimeProvider timeProvider) : IAnalyticsService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<TrainerDto>> GetTrainersWithMinExperienceAsync(
        int minYears, CancellationToken ct = default)
    {
        var specializationNames = (await specs.GetAllAsync(ct))
            .ToDictionary(s => s.Id, s => s.Name);

        return [.. (await trainers.GetAllAsync(ct))
            .Where(trainer => trainer.ExperienceYears >= minYears)
            .OrderByDescending(trainer => trainer.ExperienceYears)
            .Select(trainer => trainer.ToDto(
                specializationNames.GetValueOrDefault(trainer.SpecializationId, string.Empty)))];
    }

    /// <inheritdoc/>
    public async Task<HallAvailabilityDto> CheckHallAvailabilityAsync(
        string hallName, DateTime at, CancellationToken ct = default)
    {
        var bookingsInHall = await bookings.GetAllAsync(ct);
        var bookingsForHall = bookingsInHall.Where(booking =>
            string.Equals(booking.HallName, hallName, StringComparison.OrdinalIgnoreCase));

        var isBusy = bookingsForHall.Any(booking =>
            booking.ScheduledAt <= at
            && at < booking.ScheduledAt.AddHours(1));

        return new HallAvailabilityDto
        {
            HallName = hallName,
            At = at,
            IsAvailable = !isBusy
        };
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<ClientDto>> GetExpiredSubscriptionClientsAsync(
        CancellationToken ct = default)
    {
        var today = DateOnly.FromDateTime(timeProvider.GetLocalNow().DateTime);

        return [.. (await clients.GetAllAsync(ct))
            .Where(client => client.IsMembershipExpired(today))
            .OrderBy(client => client.FullName)
            .Select(client => client.ToDto())];
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<BookingDto>> GetBookingsInHallForMonthAsync(
        string hallName, int year, int month, CancellationToken ct = default)
        => [.. (await bookings.GetByHallAndMonthAsync(hallName, year, month, ct))
            .OrderBy(booking => booking.ScheduledAt)
            .Select(booking => booking.ToDto())];

    /// <inheritdoc/>
    public async Task<IReadOnlyList<PopularTrainerDto>> GetTopPopularTrainersAsync(
        int count, CancellationToken ct = default)
    {
        var allTrainers = await trainers.GetAllAsync(ct);
        var bookingCounts = (await bookings.GetAllAsync(ct))
            .GroupBy(booking => booking.TrainerId)
            .ToDictionary(group => group.Key, group => group.Count());

        return [.. allTrainers
            .Select(trainer => new PopularTrainerDto
            {
                TrainerId = trainer.Id,
                FullName = trainer.FullName,
                BookingsCount = bookingCounts.GetValueOrDefault(trainer.Id)
            })
            .OrderByDescending(trainer => trainer.BookingsCount)
            .ThenBy(trainer => trainer.FullName)
            .Take(count)];
    }
}
