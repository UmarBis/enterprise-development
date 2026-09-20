namespace FitnessClub.Domain.Entities;

/// <summary>
/// Групповая тренировка.
/// </summary>
public class Workout
{
    /// <summary>Идентификатор тренировки.</summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>Название тренировки.</summary>
    public required string Name { get; init; }

    /// <summary>Идентификатор тренера.</summary>
    public Guid TrainerId { get; init; }

    /// <summary>Дата и время начала в UTC.</summary>
    public DateTime ScheduledAt { get; init; }

    /// <summary>Продолжительность.</summary>
    public TimeSpan Duration { get; init; }

    /// <summary>Максимальное число участников.</summary>
    public int MaxParticipants { get; init; }

    /// <summary>Стоимость посещения в рублях.</summary>
    public decimal Price { get; init; }
}
