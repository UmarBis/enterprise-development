namespace FitnessClub.Domain.Entities;

/// <summary>
/// Справочник специализаций тренеров.
/// </summary>
public class Specialization
{
    /// <summary>Идентификатор.</summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>Название специализации.</summary>
    public required string Name { get; init; }
}
