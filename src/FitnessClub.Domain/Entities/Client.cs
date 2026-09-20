namespace FitnessClub.Domain.Entities;

/// <summary>
/// Клиент финтес-клуба.
/// </summary>
public class Client
{
    /// <summary>Идентификатор клиента.</summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>Имя.</summary>
    public required string FirstName { get; init; }

    /// <summary>Фамилия.</summary>
    public required string LastName { get; init; }

    /// <summary>Контактный телефон.</summary>
    /// <example>+7-900-100-10-01</example>
    public required string Phone { get; init; }

    /// <summary>Электронная почта.</summary>
    public required string Email { get; init; }

    /// <summary>Дата рождения.</summary>
    public DateOnly BirthDate { get; init; }

    /// <summary>Дата регистрации в клубе.</summary>
    public DateOnly RegistrationDate { get; init; }

    /// <summary>Полное имя клиента.</summary>
    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// Возвращает количество полных лет на указанную дату.
    /// </summary>
    /// <param name="onDate">Дата, на которую вычисляется возраст.</param>
    public int GetAge(DateOnly onDate)
    {
        var age = onDate.Year - BirthDate.Year;
        if (onDate < BirthDate.AddYears(age))
        {
            age--;
        }

        return age;
    }
}
