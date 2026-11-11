namespace FitnessClub.Contracts.Dto;

/// <summary>Популярный тренер: количество записей.</summary>
public class PopularTrainerDto
{
    /// <summary>Идентификатор тренера.</summary>
    public Guid TrainerId { get; init; }

    /// <summary>ФИО тренера.</summary>
    public string FullName { get; init; } = string.Empty;

    /// <summary>Количество записей к тренеру.</summary>
    public int BookingsCount { get; init; }
}