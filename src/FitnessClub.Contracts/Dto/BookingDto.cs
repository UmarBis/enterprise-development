namespace FitnessClub.Contracts.Dto;

/// <summary>DTO записи клиента на персональное занятие.</summary>
public class BookingDto
{
    /// <summary>Уникальный идентификатор сущности.</summary>
    public Guid Id { get; init; }
    /// <summary>Идентификатор клиента.</summary>
    public Guid ClientId { get; init; }
    /// <summary>Идентификатор тренера.</summary>
    public Guid TrainerId { get; init; }
    /// <summary>Дата и время занятия.</summary>
    public DateTime SessionDateTime { get; init; }
    /// <summary>Название зала.</summary>
    public string HallName { get; init; } = string.Empty;
    /// <summary>Признак пробного занятия.</summary>
    public bool IsTrial { get; init; }
}
