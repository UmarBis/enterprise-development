using FitnessClub.Domain.Entities;

namespace FitnessClub.Tests;

/// <summary>
/// Тесты запроса информации о тренерах по стажу работы.
/// </summary>
public class TrainersQueryTests(FitnessClubFixture fixture) : IClassFixture<FitnessClubFixture>
{
    /// <summary>
    /// Должен возвращать только тренеров, стаж работы которых не менее 5 лет.
    /// </summary>
    [Fact]
    public void TrainersWithAtLeastFiveYearsExperienceShouldBeReturned()
    {
        // arrange
        string[] expectedTrainerNames =
        [
            "Кузнецова Наталья",
            "Лебедева Дарья",
            "Новиков Артур",
            "Морозов Георгий",
            "Волкова Ирина",
            "Павлова Светлана",
            "Семёнов Кирилл",
            "Голубева Вероника",
        ];

        // act
        IReadOnlyList<Trainer> actualTrainers =
        [
            .. fixture.Trainers.Where(t => t.ExperienceYears >= 5)
        ];

        // assert
        Assert.Equal(expectedTrainerNames, actualTrainers.Select(t => t.FullName));
    }
}