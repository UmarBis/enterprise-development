using FitnessClub.Domain.Enums;

namespace FitnessClub.Domain.Entities;

/// <summary>
/// Базовый класс персоны, объединяющий общие свойства клиентов и тренеров.
/// </summary>
public abstract class Person
{
    /// <summary>Идентификатор.</summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>Номер паспорта.</summary>
    public required string PassportNumber { get; init; }

    /// <summary>Имя.</summary>
    public required string FirstName { get; init; }

    /// <summary>Фамилия.</summary>
    public required string LastName { get; init; }

    /// <summary>Пол.</summary>
    public Gender Gender { get; init; }

    /// <summary>Дата рождения.</summary>
    public DateOnly BirthDate { get; init; }

    /// <summary>Полное имя в формате «Фамилия Имя».</summary>
    public string FullName => $"{LastName} {FirstName}";
}
