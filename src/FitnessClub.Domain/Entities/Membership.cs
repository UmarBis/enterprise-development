using FitnessClub.Domain.Enums;

namespace FitnessClub.Domain.Entities;

/// <summary>
/// Абонемент клиента финтес-клуба.
/// </summary>
public class Membership
{
    /// <summary>Идентификатор абонемента.</summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>Идентификатор клиента-владельца.</summary>
    public Guid ClientId { get; init; }

    /// <summary>Тип абонемента.</summary>
    public MembershipType Type { get; init; }

    /// <summary>Дата начала действия.</summary>
    public DateOnly StartDate { get; init; }

    /// <summary>Дата окончания действия.</summary>
    public DateOnly EndDate { get; init; }

    /// <summary>Стоимость абонемента в рублях.</summary>
    public decimal Price { get; init; }

    /// <summary>
    /// Проверяет, активен ли абонемент на указанную дату.
    /// </summary>
    /// <param name="onDate">Дата проверки.</param>
    public bool IsActive(DateOnly onDate) => onDate >= StartDate && onDate <= EndDate;
}
