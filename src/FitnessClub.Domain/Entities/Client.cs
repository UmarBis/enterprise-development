namespace FitnessClub.Domain.Entities;

/// <summary>
/// Клиент фитнес-клуба.
/// </summary>
public class Client : Person
{
    /// <summary>Контактный телефон.</summary>
    public required string Phone { get; init; }

    /// <summary>Дата начала абонемента.</summary>
    public DateOnly MembershipStartDate { get; init; }

    /// <summary>Дата окончания абонемента.</summary>
    public DateOnly MembershipEndDate { get; init; }

    /// <summary>Проверяет, активен ли абонемент на указанную дату.</summary>
    public bool IsMembershipActive(DateOnly onDate)
        => onDate >= MembershipStartDate && onDate <= MembershipEndDate;

    /// <summary>Проверяет, просрочен ли абонемент на указанную дату.</summary>
    public bool IsMembershipExpired(DateOnly onDate) => onDate > MembershipEndDate;
}
