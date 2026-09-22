namespace FitnessClub.Domain.Entities;

/// <summary>
/// Тренер фитнес-клуба.
/// </summary>
public class Trainer : Person
{
    /// <summary>Идентификатор специализации (ссылка на справочник).</summary>
    public Guid SpecializationId { get; init; }

    /// <summary>Стаж работы в годах.</summary>
    public int ExperienceYears { get; init; }
}
