using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>DTO тренера фитнес-клуба: данные для создания и обновления.</summary>
public class UpsertTrainerDto
{
    [Required(ErrorMessage = "Не указан номер паспорта")]
    public string PassportNumber { get; init; } = string.Empty;

    [Required(ErrorMessage = "Не указано имя")]
    public string FirstName { get; init; } = string.Empty;

    [Required(ErrorMessage = "Не указана фамилия")]
    public string LastName { get; init; } = string.Empty;

    [Required(ErrorMessage = "Не указан пол")]
    [EnumDataType(typeof(Gender), ErrorMessage = "Некорректное значение пола")]
    public Gender? Gender { get; init; }

    [Required(ErrorMessage = "Не указана дата рождения")]
    public DateOnly? BirthDate { get; init; }

    [Required(ErrorMessage = "Не указана специализация")]
    public Guid? SpecializationId { get; init; }

    [Range(0, 100, ErrorMessage = "Стаж работы должен быть от 0 до 100 лет")]
    public int ExperienceYears { get; init; }
}