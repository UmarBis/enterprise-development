using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Seeds;

/// <summary>
/// Набор данных фитнес-клуба для наполнения хранилища в памяти.
/// </summary>
public class ClubData
{
    /// <summary>Справочник специализаций тренеров.</summary>
    public required IReadOnlyList<Specialization> Specializations { get; init; }

    /// <summary>Клиенты фитнес-клуба.</summary>
    public required IReadOnlyList<Client> Clients { get; init; }

    /// <summary>Тренеры фитнес-клуба.</summary>
    public required IReadOnlyList<Trainer> Trainers { get; init; }

    /// <summary>Записи клиентов на персональные занятия.</summary>
    public required IReadOnlyList<Booking> Bookings { get; init; }
}