namespace FitnessClub.Contracts.Dto;

/// <summary>Результат проверки доступности зала.</summary>
public class HallAvailabilityDto
{
    /// <summary>Название зала.</summary>
    public string HallName { get; init; } = string.Empty;

    /// <summary>Момент времени проверки.</summary>
    public DateTime At { get; init; }

    /// <summary>Свободен ли зал в указанное время.</summary>
    public bool IsAvailable { get; init; }
}