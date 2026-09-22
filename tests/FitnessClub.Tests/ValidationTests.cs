using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Tests;

/// <summary>
/// Проверки бизнес-правил персон, абонементов и записей.
/// </summary>
public class ValidationTests
{
    /// <summary>Абонемент считается активным включительно по границам.</summary>
    [Theory]
    [InlineData("2025-01-01", true)]
    [InlineData("2025-06-15", true)]
    [InlineData("2025-12-31", true)]
    [InlineData("2024-12-31", false)]
    [InlineData("2026-01-01", false)]
    public void ClientMembershipActivityBoundaries(string dateString, bool expected)
    {
        // Arrange
        var client = new Client
        {
            PassportNumber      = "2000 999999",
            FirstName           = "Тест",
            LastName            = "Тестов",
            Gender              = Gender.Male,
            BirthDate           = new DateOnly(1990, 1, 1),
            Phone               = "+7-900-000-00-00",
            MembershipStartDate = new DateOnly(2025, 1, 1),
            MembershipEndDate   = new DateOnly(2025, 12, 31),
        };
        var date = DateOnly.Parse(dateString);

        // Act
        var active = client.IsMembershipActive(date);

        // Assert
        Assert.Equal(expected, active);
    }

    /// <summary>Просроченный абонемент определяется корректно.</summary>
    [Fact]
    public void ClientWithExpiredMembershipIsDetected()
    {
        // Arrange
        var client = new Client
        {
            PassportNumber      = "2000 999998",
            FirstName           = "Пётр",
            LastName            = "Просроченко",
            Gender              = Gender.Male,
            BirthDate           = new DateOnly(1985, 5, 5),
            Phone               = "+7-900-000-00-01",
            MembershipStartDate = new DateOnly(2023, 1, 1),
            MembershipEndDate   = new DateOnly(2024, 1, 1),
        };

        // Act
        var expired = client.IsMembershipExpired(new DateOnly(2025, 1, 15));

        // Assert
        Assert.True(expired);
    }

    /// <summary>Полное имя формируется в формате «Фамилия Имя».</summary>
    [Fact]
    public void PersonFullNameContainsBothParts()
    {
        // Arrange
        var trainer = new Trainer
        {
            PassportNumber   = "1000 999999",
            FirstName        = "Анна",
            LastName         = "Смирнова",
            Gender           = Gender.Female,
            BirthDate        = new DateOnly(1990, 3, 3),
            SpecializationId = Guid.NewGuid(),
            ExperienceYears  = 7,
        };

        // Act
        var fullName = trainer.FullName;

        // Assert
        Assert.Equal("Смирнова Анна", fullName);
    }

    /// <summary>Пробное занятие помечается флагом IsTrial.</summary>
    [Fact]
    public void TrialBookingHasTrialFlagSet()
    {
        // Arrange
        var booking = new Booking
        {
            ClientId    = Guid.NewGuid(),
            TrainerId   = Guid.NewGuid(),
            HallName    = "Зал А",
            ScheduledAt = new DateTime(2025, 1, 20, 10, 0, 0, DateTimeKind.Utc),
            IsTrial     = true,
        };

        // Act & Assert
        Assert.True(booking.IsTrial);
    }
}
