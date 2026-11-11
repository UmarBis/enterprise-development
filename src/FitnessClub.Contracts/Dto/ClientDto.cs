using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>DTO клиента фитнес-клуба: данные в ответе сервера.</summary>
public class ClientDto
{
    public Guid Id { get; init; }
    public string PassportNumber { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public Gender Gender { get; init; }
    public DateOnly BirthDate { get; init; }
    public string Phone { get; init; } = string.Empty;
    public DateOnly SubscriptionStart { get; init; }
    public DateOnly SubscriptionEnd { get; init; }
}