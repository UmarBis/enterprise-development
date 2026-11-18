namespace FitnessClub.Contracts.Dto;

/// <summary>DTO специализации тренера (элемент справочника).</summary>
public class SpecializationDto
{
    /// <summary>Уникальный идентификатор сущности.</summary>
    public Guid Id { get; init; }
    /// <summary>Название специализации.</summary>
    public string Name { get; init; } = string.Empty;
}
