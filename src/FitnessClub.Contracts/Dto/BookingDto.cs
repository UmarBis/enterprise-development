namespace FitnessClub.Contracts.Dto;

/// <summary>DTO записи клиента на персональное занятие.</summary>
public class BookingDto
{
    public Guid Id { get; init; }
    public Guid ClientId { get; init; }
    public Guid TrainerId { get; init; }
    public DateTime SessionDateTime { get; init; }
    public string HallName { get; init; } = string.Empty;
    public bool IsTrial { get; init; }
}