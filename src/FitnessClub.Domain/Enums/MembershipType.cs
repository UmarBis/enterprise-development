namespace FitnessClub.Domain.Enums;

/// <summary>
/// Тип абонемента финтес-клуба.
/// </summary>
public enum MembershipType
{
    /// <summary>Месячный абонемент.</summary>
    Monthly = 1,

    /// <summary>Квартальный абонемент (3 месяца).</summary>
    Quarterly = 2,

    /// <summary>Годовой абонемент.</summary>
    Yearly = 3,
}
