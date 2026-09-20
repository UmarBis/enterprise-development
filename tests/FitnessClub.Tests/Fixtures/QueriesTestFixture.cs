using FitnessClub.Domain.Entities;

namespace FitnessClub.Tests.Fixtures;

/// <summary>
/// Фикстура, разделяющая один набор тестовых данных между всеми тестами.
/// </summary>
public class QueriesTestFixture
{
    /// <summary>Клиенты.</summary>
    public List<Client> Clients { get; }

    /// <summary>Абонементы.</summary>
    public List<Membership> Memberships { get; }

    /// <summary>Тренеры.</summary>
    public List<Trainer> Trainers { get; }

    /// <summary>Тренировки.</summary>
    public List<Workout> Workouts { get; }

    /// <summary>Посещения.</summary>
    public List<Visit> Visits { get; }

    /// <summary>
    /// Инициализирует фикстуру, генерируя тестовый набор данных один раз.
    /// </summary>
    public QueriesTestFixture()
    {
        var seed = DataSeeder.Seed();

        Clients = seed.Clients;
        Memberships = seed.Memberships;
        Trainers = seed.Trainers;
        Workouts = seed.Workouts;
        Visits = seed.Visits;
    }
}
