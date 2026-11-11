using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>DTO тренера фитнес-клуба: данные в ответе сервера.</summary>
public class TrainerDto
{
    public Guid Id { get; init; }
    public string PassportNumber { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public Gender Gender { get; init; }
    public DateOnly BirthDate { get; init; }
    public Guid SpecializationId { get; init; }
    public string SpecializationName { get; init; } = string.Empty;
    public int ExperienceYears { get; init; }
}