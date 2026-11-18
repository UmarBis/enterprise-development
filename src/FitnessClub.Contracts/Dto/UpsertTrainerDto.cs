using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>DTO тренера фитнес-клуба: данные для создания и обновления.</summary>
public class UpsertTrainerDto
{
    /// <summary>Номер паспорта.</summary>
    [Required(ErrorMessage = "Не указан номер паспорта")]
    public string PassportNumber { get; init; } = string.Empty;

    /// <summary>Имя.</summary>
    [Required(ErrorMessage = "Не указано имя")]
    public string FirstName { get; init; } = string.Empty;

    /// <summary>Фамилия.</summary>
    [Required(ErrorMessage = "Не указана фамилия")]
    public string LastName { get; init; } = string.Empty;

    /// <summary>Пол.</summary>
    [Required(ErrorMessage = "Не указан пол")]
    [EnumDataType(typeof(Gender), ErrorMessage = "Некорректное значение пола")]
    public Gender? Gender { get; init; }

    /// <summary>Дата рождения.</summary>
    [Required(ErrorMessage = "Не указана дата рождения")]
    public DateOnly? BirthDate { get; init; }

    /// <summary>Идентификатор специализации тренера.</summary>
    [Required(ErrorMessage = "Не указана специализация")]
    public Guid? SpecializationId { get; init; }

    /// <summary>Стаж работы тренера в годах.</summary>
    [Range(0, 100, ErrorMessage = "Стаж работы должен быть от 0 до 100 лет")]
    public int ExperienceYears { get; init; }
}
