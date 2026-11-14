using FitnessClub.Application.Services;
using FitnessClub.Domain.Seeds;
using FitnessClub.Infrastructure.InMemory;
using Microsoft.Extensions.Time.Testing;

namespace FitnessClub.Tests;

/// <summary>Проверки аналитики с управляемым временем.</summary>
public class AnalyticsServiceTests
{
    /// <summary>Сервис использует внедрённый TimeProvider при поиске просроченных абонементов.</summary>
    [Fact]
    public async Task ExpiredSubscriptionsUseInjectedTimeProvider()
    {
        var timeProvider = new FakeTimeProvider(
            new DateTimeOffset(2026, 11, 13, 12, 0, 0, TimeSpan.Zero));
        var data = ClubDataSeeder.Create(timeProvider.GetLocalNow().DateTime);

        var service = new AnalyticsService(
            new InMemoryTrainerRepository(data.Trainers),
            new InMemorySpecializationRepository(data.Specializations),
            new InMemoryBookingRepository(data.Bookings),
            new InMemoryClientRepository(data.Clients),
            timeProvider);

        var today = DateOnly.FromDateTime(timeProvider.GetLocalNow().DateTime);
        var expectedIds = data.Clients
            .Where(client => client.IsMembershipExpired(today))
            .OrderBy(client => client.FullName)
            .Select(client => client.Id);

        var actual = await service.GetExpiredSubscriptionClientsAsync(TestContext.Current.CancellationToken);

        Assert.Equal(expectedIds, actual.Select(client => client.Id));
    }
}
