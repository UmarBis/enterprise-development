namespace FitnessClub.Domain.Entities;

/// <summary>
/// Факт посещения тренировки клиентом.
/// </summary>
public class Visit
{
    /// <summary>Идентификатор посещения.</summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>Идентификатор клиента.</summary>
    public Guid ClientId { get; init; }

    /// <summary>Идентификатор тренировки.</summary>
    public Guid WorkoutId { get; init; }

    /// <summary>Момент посещения в UTC.</summary>
    public DateTime VisitTime { get; init; }

    /// <summary>Признак: клиент действительно пришёл на тренировку.</summary>
    public bool Attended { get; init; }
}
