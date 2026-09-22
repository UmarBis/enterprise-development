using FitnessClub.Domain.Enums;

namespace FitnessClub.Tests;

/// <summary>
/// Проверки корректности генерации тестовых данных.
/// </summary>
/// <param name="fixture">Общая фикстура с тестовыми данными.</param>
public class DataSeedTests(QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
{
    /// <summary>Сидер создаёт не менее 10 экземпляров каждого класса.</summary>
    [Fact]
    public void DataSeedShouldContainAtLeast10OfEach()
    {
        // Assert
        Assert.True(fixture.Clients.Count >= 10);
        Assert.True(fixture.Trainers.Count >= 10);
        Assert.True(fixture.Bookings.Count >= 10);
        Assert.True(fixture.Specializations.Count >= 10);
    }

    /// <summary>У каждого тренера специализация ссылается на существующий справочник.</summary>
    [Fact]
    public void EveryTrainerHasValidSpecialization()
    {
        // Arrange
        var specIds = fixture.Specializations.Select(s => s.Id).ToHashSet();

        // Act
        var broken = fixture.Trainers.Where(t => !specIds.Contains(t.SpecializationId)).ToList();

        // Assert
        Assert.Empty(broken);
    }

    /// <summary>Каждая запись ссылается на существующих клиента и тренера.</summary>
    [Fact]
    public void EveryBookingReferencesExistingEntities()
    {
        // Arrange
        var clientIds  = fixture.Clients.Select(c => c.Id).ToHashSet();
        var trainerIds = fixture.Trainers.Select(t => t.Id).ToHashSet();

        // Act
        var broken = fixture.Bookings
            .Where(b => !clientIds.Contains(b.ClientId) || !trainerIds.Contains(b.TrainerId))
            .ToList();

        // Assert
        Assert.Empty(broken);
    }

    /// <summary>Есть и мужчины, и женщины среди клиентов и тренеров.</summary>
    [Fact]
    public void BothGendersArePresent()
    {
        // Act
        var clientHasBoth  = fixture.Clients.Any(c => c.Gender == Gender.Male) && fixture.Clients.Any(c => c.Gender == Gender.Female);
        var trainerHasBoth = fixture.Trainers.Any(t => t.Gender == Gender.Male) && fixture.Trainers.Any(t => t.Gender == Gender.Female);

        // Assert
        Assert.True(clientHasBoth);
        Assert.True(trainerHasBoth);
    }

    /// <summary>Все записи имеют заполненное название зала.</summary>
    [Fact]
    public void EveryBookingHasHallName()
    {
        // Act
        var broken = fixture.Bookings.Where(b => string.IsNullOrWhiteSpace(b.HallName)).ToList();

        // Assert
        Assert.Empty(broken);
    }
}
