namespace FitnessClub.Tests;

/// <summary>
/// Тесты запроса информации о клиентах с истёкшим абонементом.
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными фитнес-клуба.</param>
public class ExpiredSubscriptionTests(QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
{
    /// <summary>
    /// Должен возвращать только клиентов с просроченным абонементом, упорядоченных по ФИО.
    /// </summary>
    [Fact]
    public void ExpiredSubscriptionClientsShouldBeReturnedOrderedByFullName()
    {
        // arrange
        var today = fixture.Today;
        string[] expectedClientNames =
        [
            "Егоров Николай",
            "Иванов Иван",
            "Морозова Ольга",
            "Новиков Сергей",
            "Попов Дмитрий",
            "Смирнов Алексей",
        ];

        // act
        var actualClients = fixture.Clients
            .Where(c => c.MembershipEndDate < today)
            .OrderBy(c => c.FullName)
            .ToList();

        // assert
        Assert.Equal(expectedClientNames, actualClients.Select(c => c.FullName));
    }
}
