using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>DTO тренера фитнес-клуба: данные в ответе сервера.</summary>
public class TrainerDto
{
    /// <summary>Уникальный идентификатор сущности.</summary>
    public Guid Id { get; init; }
    /// <summary>Номер паспорта.</summary>
    public string PassportNumber { get; init; } = string.Empty;
    /// <summary>Полное имя.</summary>
    public string FullName { get; init; } = string.Empty;
    /// <summary>Пол.</summary>
    public Gender Gender { get; init; }
    /// <summary>Дата рождения.</summary>
    public DateOnly BirthDate { get; init; }
    /// <summary>Идентификатор специализации тренера.</summary>
    public Guid SpecializationId { get; init; }
    /// <summary>Название специализации тренера.</summary>
    public string SpecializationName { get; init; } = string.Empty;
    /// <summary>Стаж работы тренера в годах.</summary>
    public int ExperienceYears { get; init; }
}
