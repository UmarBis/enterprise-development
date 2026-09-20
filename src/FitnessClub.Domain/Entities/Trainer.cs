namespace FitnessClub.Domain.Entities;

/// <summary>
/// Тренер финтес-клуба.
/// </summary>
public class Trainer
{
    /// <summary>Идентификатор тренера.</summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>Имя.</summary>
    public required string FirstName { get; init; }

    /// <summary>Фамилия.</summary>
    public required string LastName { get; init; }

    /// <summary>Специализация (направление тренировок).</summary>
    /// <example>Йога</example>
    public required string Specialization { get; init; }

    /// <summary>Стаж работы в годах.</summary>
    public int ExperienceYears { get; init; }

    /// <summary>Полное имя тренера.</summary>
    public string FullName => $"{FirstName} {LastName}";
}
