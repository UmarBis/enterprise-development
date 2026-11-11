using FitnessClub.Domain.Seeds;

namespace FitnessClub.Tests;

/// <summary>
/// Тесты проверки доступности зала для записи.
/// </summary>
public class HallAvailabilityTests(FitnessClubFixture fixture) : IClassFixture<FitnessClubFixture>
{
    /// <summary>
    /// Должен определить, что зал недоступен, если в нём идёт занятие, пересекающееся
    /// по времени с текущим моментом, и доступен, если занятия нет.
    /// </summary>
    /// <param name="hallName">Название проверяемого зала.</param>
    /// <param name="expectedAvailability">Ожидаемая доступность зала.</param>
    [Theory]
    [InlineData("Зал групповых занятий", false)] // прямо сейчас идёт занятие
    [InlineData("Зал единоборств", true)]         // занятие в зале закончилось 10 минут назад
    [InlineData("Кардио-зал", true)]              // занятий в зале ещё не было
    public void HallAvailabilityShouldBeDeterminedByOverlappingSession(string hallName, bool expectedAvailability)
    {
        // arrange
        var now = fixture.Now;
        var duration = ClubDataSeeder.SessionDuration;

        // act
        var isHallAvailable = !fixture.Bookings.Any(b =>
            b.HallName == hallName
            && b.ScheduledAt <= now
            && now < b.ScheduledAt + duration);

        // assert
        Assert.Equal(expectedAvailability, isHallAvailable);
    }
}