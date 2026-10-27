namespace FitnessClub.Tests;

/// <summary>
/// Тесты запроса информации о занятиях за текущий месяц в выбранном зале.
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными фитнес-клуба.</param>
public class SessionsQueryTests(QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
{
    /// <summary>
    /// Должен возвращать только занятия, проходящие в выбранном зале в текущем месяце,
    /// и не возвращать занятия того же зала в других месяцах и занятия других залов в текущем месяце.
    /// </summary>
    [Fact]
    public void CurrentMonthSessionsInSelectedHallShouldBeReturned()
    {
        // arrange
        var hallName = "Зал А";
        var today = fixture.Today;
        DateTime[] expectedSessionDates =
        [
            new DateTime(2025, 1, 5, 10, 0, 0, DateTimeKind.Utc),
            new DateTime(2025, 1, 8, 16, 0, 0, DateTimeKind.Utc),
            new DateTime(2025, 1, 14, 19, 0, 0, DateTimeKind.Utc),
            new DateTime(2025, 1, 15, 14, 0, 0, DateTimeKind.Utc),
            new DateTime(2025, 1, 22, 18, 0, 0, DateTimeKind.Utc),
        ];

        // act
        var actualSessions = fixture.Bookings
            .Where(s => s.HallName == hallName
                        && s.ScheduledAt.Year == today.Year
                        && s.ScheduledAt.Month == today.Month)
            .OrderBy(s => s.ScheduledAt)
            .ToList();

        // assert
        Assert.Equal(expectedSessionDates, actualSessions.Select(s => s.ScheduledAt));
    }
}
