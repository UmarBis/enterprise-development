using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dto;

/// <summary>DTO записи клиента на персональное занятие: данные для создания и обновления.</summary>
public class UpsertBookingDto
{
    [Required(ErrorMessage = "Не указан клиент")]
    public Guid? ClientId { get; init; }

    [Required(ErrorMessage = "Не указан тренер")]
    public Guid? TrainerId { get; init; }

    [Required(ErrorMessage = "Не указано время занятия")]
    public DateTime? SessionDateTime { get; init; }

    [Required(ErrorMessage = "Не указано название зала")]
    public string HallName { get; init; } = string.Empty;

    public bool IsTrial { get; init; }
}