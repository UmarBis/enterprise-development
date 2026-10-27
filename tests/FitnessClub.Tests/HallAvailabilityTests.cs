namespace FitnessClub.Tests;

/// <summary>
/// Тесты проверки доступности зала для записи.
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными фитнес-клуба.</param>
public class HallAvailabilityTests(QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
{
    /// <summary>
    /// Должен определить, что зал недоступен, если в нём есть занятие, пересекающееся по времени,
    /// и доступен, если занятий нет.
    /// </summary>
    /// <param name="hallName">Название проверяемого зала.</param>
    /// <param name="expectedAvailability">Ожидаемая доступность зала.</param>
    [Theory]
    [InlineData("Зал А", false)]   // прямо сейчас (15.01 14:00) в зале идет занятие
    [InlineData("Зал Б", true)]    // в это время в зале пусто
    [InlineData("Бассейн", true)]  // в это время в зале пусто
    public void HallAvailabilityShouldBeDeterminedByOverlappingSession(string hallName, bool expectedAvailability)
    {
        // arrange
        var now = fixture.Now;
        var duration = TimeSpan.FromHours(1);

        // act
        var isHallAvailable = !fixture.Bookings.Any(s =>
            s.HallName == hallName && 
            s.ScheduledAt < now + duration && 
            s.ScheduledAt + duration > now);

        // assert
        Assert.Equal(expectedAvailability, isHallAvailable);
    }
}
