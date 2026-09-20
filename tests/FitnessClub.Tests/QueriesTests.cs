using FitnessClub.Domain.Enums;
using FitnessClub.Tests.Fixtures;

namespace FitnessClub.Tests;

/// <summary>
/// LINQ-запросы к тестовому набору данных финтес-клуба.
/// </summary>
/// <param name="fixture">Общая фикстура с тестовыми данными.</param>
public class QueriesTests(QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
{
    /// <summary>
    /// Активные абонементы на текущую дату.
    /// </summary>
    [Fact]
    public void ActiveMembershipsShouldOnlyContainValidOnes()
    {
        // Arrange
        var today = DataSeeder.Today;

        // Act
        var active = fixture.Memberships
            .Where(m => m.IsActive(today))
            .ToList();

        // Assert
        Assert.NotEmpty(active);
        Assert.All(active, m => Assert.True(today >= m.StartDate && today <= m.EndDate));
    }

    /// <summary>
    /// Суммарная выручка от всех абонементов.
    /// </summary>
    [Fact]
    public void TotalMembershipRevenueShouldMatchSum()
    {
        // Act
        var total = fixture.Memberships.Sum(m => m.Price);
        var expected = fixture.Memberships.Select(m => m.Price).Sum();

        // Assert
        Assert.Equal(expected, total);
        Assert.True(total > 0);
    }

    /// <summary>
    /// Самый дорогой абонемент должен быть годовым.
    /// </summary>
    [Fact]
    public void MostExpensiveMembershipShouldBeYearly()
    {
        // Act
        var most = fixture.Memberships
            .OrderByDescending(m => m.Price)
            .First();

        // Assert
        Assert.Equal(MembershipType.Yearly, most.Type);
    }

    /// <summary>
    /// Опытные тренеры (стаж ≥ 5 лет) отсортированы по убыванию стажа.
    /// </summary>
    [Fact]
    public void ExperiencedTrainersShouldBeSortedByExperienceDesc()
    {
        // Act
        var experienced = fixture.Trainers
            .Where(t => t.ExperienceYears >= 5)
            .OrderByDescending(t => t.ExperienceYears)
            .ToList();

        // Assert
        Assert.NotEmpty(experienced);
        for (var i = 1; i < experienced.Count; i++)
        {
            Assert.True(experienced[i].ExperienceYears <= experienced[i - 1].ExperienceYears);
        }
    }

    /// <summary>
    /// Средняя длительность тренировки должна быть в разумных пределах.
    /// </summary>
    [Fact]
    public void AverageWorkoutDurationShouldBeReasonable()
    {
        // Act
        var averageMinutes = fixture.Workouts.Average(w => w.Duration.TotalMinutes);

        // Assert
        Assert.InRange(averageMinutes, 30, 180);
    }

    /// <summary>
    /// Группировка тренировок по тренерам сохраняет общее количество.
    /// </summary>
    [Fact]
    public void WorkoutsGroupedByTrainerShouldPreserveTotal()
    {
        // Act
        var grouped = fixture.Workouts
            .GroupBy(w => w.TrainerId)
            .Select(g => new { TrainerId = g.Key, Count = g.Count() })
            .ToList();

        // Assert
        Assert.Equal(fixture.Workouts.Count, grouped.Sum(g => g.Count));
        Assert.Equal(
            fixture.Workouts.Select(w => w.TrainerId).Distinct().Count(),
            grouped.Count);
    }

    /// <summary>
    /// Отчёт по посещениям: для каждого клиента — число его посещений.
    /// </summary>
    [Fact]
    public void ClientVisitReportShouldMatchTotalVisits()
    {
        // Act
        var report = fixture.Clients
            .GroupJoin(
                fixture.Visits,
                c => c.Id,
                v => v.ClientId,
                (c, visits) => new { Client = c.FullName, Count = visits.Count() })
            .OrderByDescending(x => x.Count)
            .ToList();

        // Assert
        Assert.Equal(fixture.Clients.Count, report.Count);
        Assert.Equal(fixture.Visits.Count, report.Sum(r => r.Count));
    }

    /// <summary>
    /// Доля фактических посещений в диапазоне от 0 до 100 %.
    /// </summary>
    [Fact]
    public void AttendanceRateShouldBeInRange()
    {
        // Act
        var total = fixture.Visits.Count;
        var attended = fixture.Visits.Count(v => v.Attended);
        var rate = 100.0 * attended / total;

        // Assert
        Assert.InRange(rate, 0, 100);
        Assert.Equal(fixture.Visits.Count(v => !v.Attended), total - attended);
    }

    /// <summary>
    /// Топ-3 самых молодых клиента отсортированы по убыванию даты рождения.
    /// </summary>
    [Fact]
    public void Top3YoungestClientsShouldBeSortedByBirthDateDesc()
    {
        // Arrange
        const int topCount = 3;

        // Act
        var youngest = fixture.Clients
            .OrderByDescending(c => c.BirthDate)
            .Take(topCount)
            .ToList();

        // Assert
        Assert.Equal(topCount, youngest.Count);
        for (var i = 1; i < youngest.Count; i++)
        {
            Assert.True(youngest[i].BirthDate <= youngest[i - 1].BirthDate);
        }
    }

    /// <summary>
    /// Объединение клиентов и абонементов: у каждого абонемента есть владелец.
    /// </summary>
    [Fact]
    public void MembershipsJoinedWithClientsShouldMatchCount()
    {
        // Act
        var joined = fixture.Memberships
            .Join(
                fixture.Clients,
                m => m.ClientId,
                c => c.Id,
                (m, c) => new { Membership = m, Client = c })
            .ToList();

        // Assert
        Assert.Equal(fixture.Memberships.Count, joined.Count);
    }

    /// <summary>
    /// Абонементы, сгруппированные по типу, покрывают все существующие типы.
    /// </summary>
    [Fact]
    public void MembershipsGroupedByTypeShouldCoverAllTypes()
    {
        // Act
        var groups = fixture.Memberships
            .GroupBy(m => m.Type)
            .ToDictionary(g => g.Key, g => g.Count());

        // Assert
        Assert.Equal(fixture.Memberships.Count, groups.Values.Sum());
        Assert.All(groups.Keys, t => Assert.True(Enum.IsDefined(t)));
    }
}
