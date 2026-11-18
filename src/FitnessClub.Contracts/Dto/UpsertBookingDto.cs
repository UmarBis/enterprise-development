using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dto;

/// <summary>DTO записи клиента на персональное занятие: данные для создания и обновления.</summary>
public class UpsertBookingDto
{
    [Required(ErrorMessage = "Не указан клиент")]
    /// <summary>Идентификатор клиента.</summary>
    public Guid? ClientId { get; init; }

    [Required(ErrorMessage = "Не указан тренер")]
    /// <summary>Идентификатор тренера.</summary>
    public Guid? TrainerId { get; init; }

    [Required(ErrorMessage = "Не указано время занятия")]
    /// <summary>Дата и время занятия.</summary>
    public DateTime? SessionDateTime { get; init; }

    [Required(ErrorMessage = "Не указано название зала")]
    [MinLength(2, ErrorMessage = "Название зала слишком короткое")]
    /// <summary>Название зала.</summary>
    public string HallName { get; init; } = string.Empty;

    /// <summary>Признак пробного занятия.</summary>
    public bool IsTrial { get; init; }
}
