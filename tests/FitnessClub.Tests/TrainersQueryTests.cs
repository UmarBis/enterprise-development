namespace FitnessClub.Tests;

/// <summary>
/// Тесты запроса информации о тренерах по стажу работы.
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными фитнес-клуба.</param>
public class TrainersQueryTests(QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
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
            "Белова Ксения",
            "Дмитриев Олег",
            "Захаров Виктор",
            "Козлов Максим",
            "Лебедева Юлия",
            "Никитин Роман",
            "Орлова Наталья",
            "Соколов Андрей",
            "Титов Артём",
        ];

        // act
        var actualTrainers = fixture.Trainers
            .Where(t => t.ExperienceYears >= 5)
            .OrderBy(t => t.FullName)
            .ToList();

        // assert
        Assert.Equal(expectedTrainerNames, actualTrainers.Select(t => t.FullName));
    }
}
