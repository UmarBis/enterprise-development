using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>DTO тренера фитнес-клуба: данные для создания и обновления.</summary>
public class UpsertTrainerDto
{
    [Required(ErrorMessage = "Не указан номер паспорта")]
    /// <summary>Номер паспорта.</summary>
    public string PassportNumber { get; init; } = string.Empty;

    [Required(ErrorMessage = "Не указано имя")]
    /// <summary>Имя.</summary>
    public string FirstName { get; init; } = string.Empty;

    [Required(ErrorMessage = "Не указана фамилия")]
    /// <summary>Фамилия.</summary>
    public string LastName { get; init; } = string.Empty;

    [Required(ErrorMessage = "Не указан пол")]
    [EnumDataType(typeof(Gender), ErrorMessage = "Некорректное значение пола")]
    /// <summary>Пол.</summary>
    public Gender? Gender { get; init; }

    [Required(ErrorMessage = "Не указана дата рождения")]
    /// <summary>Дата рождения.</summary>
    public DateOnly? BirthDate { get; init; }

    [Required(ErrorMessage = "Не указана специализация")]
    /// <summary>Идентификатор специализации тренера.</summary>
    public Guid? SpecializationId { get; init; }

    [Range(0, 100, ErrorMessage = "Стаж работы должен быть от 0 до 100 лет")]
    /// <summary>Стаж работы тренера в годах.</summary>
    public int ExperienceYears { get; init; }
}
