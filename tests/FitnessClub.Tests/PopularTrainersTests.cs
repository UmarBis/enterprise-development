namespace FitnessClub.Tests;

/// <summary>
/// Тесты определения наиболее популярных тренеров.
/// </summary>
public class PopularTrainersTests(FitnessClubFixture fixture) : IClassFixture<FitnessClubFixture>
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
            "Волкова Ирина",    // 6
            "Новиков Артур",    // 5
            "Семёнов Кирилл",   // 4
            "Кузнецова Наталья",// 3
            "Морозов Георгий",  // 2
        ];

        // act
        IReadOnlyList<string> actualTrainerNames =
        [
            .. fixture.Bookings
                .GroupBy(b => b.TrainerId)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => fixture.Trainers.First(t => t.Id == g.Key).FullName)
        ];

        // assert
        Assert.Equal(expectedTrainerNames, actualTrainerNames);
    }
}