using FitnessClub.Domain.Seeds;

namespace FitnessClub.Tests;

/// <summary>Проверяет корректность идентификаторов начальных данных.</summary>
public class SeedIdentifiersTests
{
    /// <summary>Все начальные сущности должны иметь непустые и уникальные идентификаторы.</summary>
    [Fact]
    public void SeedEntitiesShouldHaveNonEmptyUniqueIds()
    {
        var data = ClubDataSeeder.Create();
        var ids = data.Specializations.Select(x => x.Id)
            .Concat(data.Clients.Select(x => x.Id))
            .Concat(data.Trainers.Select(x => x.Id))
            .Concat(data.Bookings.Select(x => x.Id))
            .ToArray();

        Assert.All(ids, id => Assert.NotEqual(Guid.Empty, id));
        Assert.Equal(ids.Length, ids.Distinct().Count());
    }
}
