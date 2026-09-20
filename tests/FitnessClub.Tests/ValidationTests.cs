using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Tests;

/// <summary>
/// Проверки корректности обязательных свойств и бизнес-правил.
/// </summary>
public class ValidationTests
{
    /// <summary>
    /// Абонемент с датой окончания раньше начала не считается активным.
    /// </summary>
    [Fact]
    public void MembershipWithInvertedDatesShouldNotBeActive()
    {
        // Arrange
        var membership = new Membership
        {
            ClientId = Guid.NewGuid(),
            Type = MembershipType.Monthly,
            StartDate = new DateOnly(2025, 1, 1),
            EndDate = new DateOnly(2024, 1, 1),
            Price = 1000m,
        };

        // Act
        var active = membership.IsActive(DataSeeder.Today);

        // Assert
        Assert.False(active);
    }

    /// <summary>
    /// Возраст клиента на дату до дня рождения на год меньше.
    /// </summary>
    [Fact]
    public void ClientAgeBeforeBirthdayShouldBeReducedByOne()
    {
        // Arrange
        var client = new Client
        {
            FirstName = "Иван",
            LastName = "Иванов",
            Phone = "+7-900-000-00-00",
            Email = "ivanov@example.com",
            BirthDate = new DateOnly(1990, 6, 15),
            RegistrationDate = new DateOnly(2024, 1, 1),
        };

        // Act
        var ageBeforeBirthday = client.GetAge(new DateOnly(2025, 6, 14));
        var ageOnBirthday = client.GetAge(new DateOnly(2025, 6, 15));

        // Assert
        Assert.Equal(34, ageBeforeBirthday);
        Assert.Equal(35, ageOnBirthday);
    }

    /// <summary>
    /// Границы абонемента включительно считаются активными.
    /// </summary>
    [Theory]
    [InlineData("2025-01-15", true)]
    [InlineData("2025-01-01", true)]
    [InlineData("2025-12-31", true)]
    [InlineData("2024-12-31", false)]
    [InlineData("2026-01-01", false)]
    public void MembershipActivityBoundaries(string dateString, bool expected)
    {
        // Arrange
        var membership = new Membership
        {
            ClientId = Guid.NewGuid(),
            Type = MembershipType.Yearly,
            StartDate = new DateOnly(2025, 1, 1),
            EndDate = new DateOnly(2025, 12, 31),
            Price = 24000m,
        };
        var date = DateOnly.Parse(dateString);

        // Act
        var active = membership.IsActive(date);

        // Assert
        Assert.Equal(expected, active);
    }

    /// <summary>
    /// Полное имя клиента формируется через пробел.
    /// </summary>
    [Fact]
    public void ClientFullNameShouldContainBothNameParts()
    {
        // Arrange
        var client = new Client
        {
            FirstName = "Анна",
            LastName = "Сидорова",
            Phone = "+7-900-000-00-01",
            Email = "sidorova@example.com",
            BirthDate = new DateOnly(1995, 2, 20),
            RegistrationDate = new DateOnly(2024, 6, 1),
        };

        // Act
        var fullName = client.FullName;

        // Assert
        Assert.Equal("Анна Сидорова", fullName);
    }
}
