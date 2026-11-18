using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>DTO клиента фитнес-клуба: данные в ответе сервера.</summary>
public class ClientDto
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
    /// <summary>Контактный телефон.</summary>
    public string Phone { get; init; } = string.Empty;
    /// <summary>Дата начала абонемента.</summary>
    public DateOnly SubscriptionStart { get; init; }
    /// <summary>Дата окончания абонемента.</summary>
    public DateOnly SubscriptionEnd { get; init; }
}
