using FitnessClub.Domain.Entities;

namespace FitnessClub.Tests;

/// <summary>
/// Тесты запроса информации о занятиях за текущий месяц в выбранном зале.
/// </summary>
public class BookingsQueryTests(FitnessClubFixture fixture) : IClassFixture<FitnessClubFixture>
{
    /// <summary>
    /// Должен возвращать только занятия, проходящие в выбранном зале в текущем месяце,
    /// и не возвращать занятия того же зала в других месяцах и занятия других залов в текущем месяце.
    /// </summary>
    [Fact]
    public void CurrentMonthBookingsInSelectedHallShouldBeReturned()
    {
        // arrange
        var hallName = "Силовой зал";
        var today = fixture.Today;

        // act
        IReadOnlyList<Booking> actualBookings =
        [
            .. fixture.Bookings
                .Where(b => b.HallName == hallName
                            && b.ScheduledAt.Year == today.Year
                            && b.ScheduledAt.Month == today.Month)
                .OrderBy(b => b.ScheduledAt)
        ];

        // assert
        Assert.Equal(3, actualBookings.Count);
        Assert.All(actualBookings, b => Assert.Equal(hallName, b.HallName));
        Assert.All(actualBookings, b => Assert.Equal(today.Year, b.ScheduledAt.Year));
        Assert.All(actualBookings, b => Assert.Equal(today.Month, b.ScheduledAt.Month));
    }
}