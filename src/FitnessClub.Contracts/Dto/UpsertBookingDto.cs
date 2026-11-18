using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dto;

/// <summary>DTO записи клиента на персональное занятие: данные для создания и обновления.</summary>
public class UpsertBookingDto
{
    /// <summary>Идентификатор клиента.</summary>    
    [Required(ErrorMessage = "Не указан клиент")]
    public Guid? ClientId { get; init; }

    /// <summary>Идентификатор тренера.</summary>
    [Required(ErrorMessage = "Не указан тренер")]
    public Guid? TrainerId { get; init; }

    /// <summary>Дата и время занятия.</summary>
    [Required(ErrorMessage = "Не указано время занятия")]
    public DateTime? SessionDateTime { get; init; }

    /// <summary>Название зала.</summary>
    [Required(ErrorMessage = "Не указано название зала")]
    [MinLength(2, ErrorMessage = "Название зала слишком короткое")]
    public string HallName { get; init; } = string.Empty;

    /// <summary>Признак пробного занятия.</summary>
    public bool IsTrial { get; init; }
}
