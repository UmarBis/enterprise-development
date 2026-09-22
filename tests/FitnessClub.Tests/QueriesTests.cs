using FitnessClub.Domain.Entities;

namespace FitnessClub.Tests;

/// <summary>
/// LINQ-запросы к тестовому набору данных фитнес-клуба (5 тестов из ТЗ).
/// </summary>
/// <param name="fixture">Общая фикстура с тестовыми данными.</param>
public class QueriesTests(QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
{
    /// <summary>1. Вывести всех тренеров со стажем не менее 5 лет.</summary>
    [Fact]
    public void TrainersWithExperienceAtLeast5Years()
    {
        // Act
        var result = fixture.Trainers.Where(t => t.ExperienceYears >= 5).ToList();

        // Assert
        Assert.NotEmpty(result);
        Assert.All(result, t => Assert.True(t.ExperienceYears >= 5));
    }

    /// <summary>2. Проверить, доступен ли зал для записи в указанный момент времени.</summary>
    [Fact]
    public void HallIsAvailableAtGivenMoment()
    {
        // Arrange
        var hall = DataSeeder.HallB;
        var at = new DateTime(2025, 1, 15, 12, 0, 0, DateTimeKind.Utc);
        var duration = TimeSpan.FromHours(1);

        // Act
        var available = IsHallAvailable(fixture.Bookings, hall, at, duration);

        // Assert
        Assert.True(available);
    }

    /// <summary>2b. Если в зале уже есть занятие — зал занят.</summary>
    [Fact]
    public void HallIsNotAvailableWhenBooked()
    {
        // Arrange
        var existing = fixture.Bookings[0];
        var duration = TimeSpan.FromHours(1);

        // Act
        var available = IsHallAvailable(fixture.Bookings, existing.HallName, existing.ScheduledAt, duration);

        // Assert
        Assert.False(available);
    }

    /// <summary>3. Клиенты с просроченным абонементом, упорядоченные по ФИО.</summary>
    [Fact]
    public void ClientsWithExpiredMembershipOrderedByFullName()
    {
        // Arrange
        var today = DataSeeder.Today;

        // Act
        var result = fixture.Clients
            .Where(c => c.IsMembershipExpired(today))
            .OrderBy(c => c.FullName)
            .ToList();

        // Assert
        Assert.NotEmpty(result);
        for (var i = 1; i < result.Count; i++)
        {
            var cmp = string.Compare(result[i - 1].FullName, result[i].FullName, StringComparison.Ordinal);
            Assert.True(cmp <= 0);
        }
    }

    /// <summary>4. Занятия за текущий месяц в выбранном зале.</summary>
    [Fact]
    public void BookingsInCurrentMonthForSelectedHall()
    {
        // Arrange
        var hall = DataSeeder.HallA;
        var today = DataSeeder.Today;

        // Act
        var result = fixture.Bookings
            .Where(b => b.HallName == hall)
            .Where(b => b.ScheduledAt.Year == today.Year && b.ScheduledAt.Month == today.Month)
            .OrderBy(b => b.ScheduledAt)
            .ToList();

        // Assert
        Assert.NotEmpty(result);
        Assert.All(result, b =>
        {
            Assert.Equal(hall, b.HallName);
            Assert.Equal(today.Year, b.ScheduledAt.Year);
            Assert.Equal(today.Month, b.ScheduledAt.Month);
        });
    }

    /// <summary>5. Топ-5 наиболее популярных тренеров по количеству записей.</summary>
    [Fact]
    public void Top5MostPopularTrainers()
    {
        // Act
        var top5 = fixture.Bookings
            .GroupBy(b => b.TrainerId)
            .Select(g => new { TrainerId = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        // Assert
        Assert.Equal(5, top5.Count);
        for (var i = 1; i < top5.Count; i++)
        {
            Assert.True(top5[i].Count <= top5[i - 1].Count);
        }
    }

    private static bool IsHallAvailable(IEnumerable<Booking> bookings, string hallName, DateTime at, TimeSpan duration)
    {
        var end = at + duration;
        return !bookings.Any(b => b.HallName == hallName && b.ScheduledAt < end && b.ScheduledAt + duration > at);
    }
}
