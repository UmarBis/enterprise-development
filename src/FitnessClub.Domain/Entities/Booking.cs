namespace FitnessClub.Domain.Entities;

/// <summary>
/// Запись клиента на персональное занятие к тренеру.
/// В дальнейшем используется в качестве контракта.
/// </summary>
public class Booking
{
    /// <summary>Идентификатор.</summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>Идентификатор клиента.</summary>
    public Guid ClientId { get; init; }

    /// <summary>Идентификатор тренера.</summary>
    public Guid TrainerId { get; init; }

    /// <summary>Название зала.</summary>
    public required string HallName { get; init; }

    /// <summary>Дата и время занятия (UTC).</summary>
    public DateTime ScheduledAt { get; init; }

    /// <summary>Признак пробного занятия.</summary>
    public bool IsTrial { get; init; }
}
