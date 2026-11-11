namespace FitnessClub.Contracts.Dto;

/// <summary>DTO специализации тренера (элемент справочника).</summary>
public class SpecializationDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
}