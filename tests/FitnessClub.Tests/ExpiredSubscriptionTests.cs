using FitnessClub.Domain.Entities;

namespace FitnessClub.Tests;

/// <summary>
/// Тесты запроса информации о клиентах с истёкшим абонементом.
/// </summary>
public class ExpiredSubscriptionTests(FitnessClubFixture fixture) : IClassFixture<FitnessClubFixture>
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
            "Андреев Алексей",
            "Борисова Марина",
            "Васильев Дмитрий",
            "Гаврилова Елена",
            "Дмитриев Павел",
        ];

        // act
        IReadOnlyList<Client> actualClients =
        [
            .. fixture.Clients
                .Where(c => c.MembershipEndDate < today)
                .OrderBy(c => c.FullName)
        ];

        // assert
        Assert.Equal(expectedClientNames, actualClients.Select(c => c.FullName));
    }
}