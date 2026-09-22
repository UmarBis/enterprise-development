using FitnessClub.Domain.Entities;

namespace FitnessClub.Tests.Fixtures;

/// <summary>
/// Фикстура, разделяющая один набор тестовых данных между всеми тестами.
/// </summary>
public class QueriesTestFixture
{
    /// <summary>Справочник специализаций.</summary>
    public List<Specialization> Specializations { get; }

    /// <summary>Клиенты.</summary>
    public List<Client> Clients { get; }

    /// <summary>Тренеры.</summary>
    public List<Trainer> Trainers { get; }

    /// <summary>Записи на занятия.</summary>
    public List<Booking> Bookings { get; }

    /// <summary>Инициализирует фикстуру, генерируя тестовый набор данных один раз.</summary>
    public QueriesTestFixture()
    {
        var seed = DataSeeder.Seed();

        Specializations = seed.Specializations;
        Clients = seed.Clients;
        Trainers = seed.Trainers;
        Bookings = seed.Bookings;
    }
}
