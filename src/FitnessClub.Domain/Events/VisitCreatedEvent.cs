namespace FitnessClub.Domain.Events;

/// <summary>
/// Событие о создании посещения.
/// Публикуется генератором данных в Kafka-топик <c>fitness-visits</c>
/// и потребляется сервером API (лабораторные №3–4).
/// </summary>
/// <param name="VisitId">Идентификатор посещения (идемпотентность по нему).</param>
/// <param name="ClientId">Идентификатор клиента.</param>
/// <param name="WorkoutId">Идентификатор тренировки.</param>
/// <param name="VisitTime">Момент посещения в UTC.</param>
/// <param name="Attended">Признак фактического посещения.</param>
public record VisitCreatedEvent(
    Guid VisitId,
    Guid ClientId,
    Guid WorkoutId,
    DateTime VisitTime,
    bool Attended);
