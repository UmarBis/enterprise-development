using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// DTO специализации тренера (элемент справочника).
/// </summary>
public class UpsertSpecializationDto
{
    /// <summary>
    /// Название специализации.
    /// </summary>
    [Required(ErrorMessage = "Не указано название специализации")]
    [MinLength(2, ErrorMessage = "Название специализации слишком короткое")]
    public string Name { get; init; } = string.Empty;
}