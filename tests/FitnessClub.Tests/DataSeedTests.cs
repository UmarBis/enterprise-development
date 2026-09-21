namespace FitnessClub.Tests;

/// <summary>
/// Проверки того, что сидер создаёт корректное количество связанных данных.
/// </summary>
/// <param name="fixture">Общая фикстура с тестовыми данными.</param>
public class DataSeedTests(QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
{
    /// <summary>
    /// Сидер должен создавать не менее 10 экземпляров каждого класса.
    /// </summary>
    [Fact]
    public void DataSeedShouldContainAtLeast10OfEach()
    {
        // Assert
        Assert.True(fixture.Clients.Count >= 10);
        Assert.True(fixture.Memberships.Count >= 10);
        Assert.True(fixture.Trainers.Count >= 10);
        Assert.True(fixture.Workouts.Count >= 10);
        Assert.True(fixture.Visits.Count >= 10);
    }

    /// <summary>
    /// Все абонементы должны ссылаться на существующего клиента.
    /// </summary>
    [Fact]
    public void EveryMembershipShouldReferenceExistingClient()
    {
        // Arrange
        var clientIds = fixture.Clients.Select(c => c.Id).ToHashSet();

        // Act
        var orphans = fixture.Memberships
            .Where(m => !clientIds.Contains(m.ClientId))
            .ToList();

        // Assert
        Assert.Empty(orphans);
    }

    /// <summary>
    /// Все посещения должны ссылаться на существующих клиентов и тренировки.
    /// </summary>
    [Fact]
    public void EveryVisitShouldReferenceExistingEntities()
    {
        // Arrange
        var clientIds = fixture.Clients.Select(c => c.Id).ToHashSet();
        var workoutIds = fixture.Workouts.Select(w => w.Id).ToHashSet();

        // Act
        var broken = fixture.Visits
            .Where(v => !clientIds.Contains(v.ClientId) || !workoutIds.Contains(v.WorkoutId))
            .ToList();

        // Assert
        Assert.Empty(broken);
    }

    /// <summary>
    /// Все тренировки должны ссылаться на существующего тренера.
    /// </summary>
    [Fact]
    public void EveryWorkoutShouldReferenceExistingTrainer()
    {
        // Arrange
        var trainerIds = fixture.Trainers.Select(t => t.Id).ToHashSet();

        // Act
        var broken = fixture.Workouts
            .Where(w => !trainerIds.Contains(w.TrainerId))
            .ToList();

        // Assert
        Assert.Empty(broken);
    }
}
