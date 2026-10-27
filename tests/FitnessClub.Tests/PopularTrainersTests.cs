namespace FitnessClub.Tests;

/// <summary>
/// Тесты определения наиболее популярных тренеров.
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными фитнес-клуба.</param>
public class PopularTrainersTests(QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
{
    /// <summary>
    /// Должен возвращать пять тренеров с наибольшим количеством записанных занятий,
    /// упорядоченных по убыванию популярности.
    /// </summary>
    [Fact]
    public void TopFivePopularTrainersShouldBeReturnedOrderedByPopularity()
    {
        // arrange
        string[] expectedTrainerNames =
        [
            "Соколов Андрей",
            "Орлова Наталья",
            "Захаров Виктор",
            "Лебедева Юлия",
            "Козлов Максим",
        ];

        // act
        var trainersById = fixture.Trainers.ToDictionary(t => t.Id, t => t.FullName);
        var actualTrainerNames = fixture.Bookings
            .GroupBy(s => s.TrainerId)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => trainersById[g.Key]) // Сортируем по алфавиту при равенстве записей
            .Take(5)
            .Select(g => trainersById[g.Key])
            .ToList();

        // assert
        Assert.Equal(expectedTrainerNames, actualTrainerNames);
    }
}
