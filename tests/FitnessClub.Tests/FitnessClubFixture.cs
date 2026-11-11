using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Seeds;

namespace FitnessClub.Tests;

/// <summary>
/// Фикстура, содержащая тестовые данные фитнес-клуба.
/// </summary>
public class FitnessClubFixture
{
    /// <summary>Момент времени, относительно которого построены тестовые данные.</summary>
    public DateTime Now { get; }

    /// <summary>Дата, относительно которой построены тестовые данные.</summary>
    public DateOnly Today { get; }

    /// <summary>Набор данных фитнес-клуба.</summary>
    public ClubData Data { get; }

    public IReadOnlyList<Specialization> Specializations => Data.Specializations;
    public IReadOnlyList<Client> Clients => Data.Clients;
    public IReadOnlyList<Trainer> Trainers => Data.Trainers;
    public IReadOnlyList<Booking> Bookings => Data.Bookings;

    public FitnessClubFixture()
    {
        Now = DateTime.Now;
        Today = DateOnly.FromDateTime(Now);
        Data = ClubDataSeeder.Create(Now);
    }
}