using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>DTO клиента фитнес-клуба: данные для создания и обновления.</summary>
public class UpsertClientDto : IValidatableObject
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

    [Required(ErrorMessage = "Не указан телефон")]
    [Phone(ErrorMessage = "Некорректный номер телефона")]
    /// <summary>Контактный телефон.</summary>
    public string Phone { get; init; } = string.Empty;

    [Required(ErrorMessage = "Не указана дата начала абонемента")]
    /// <summary>Дата начала абонемента.</summary>
    public DateOnly? SubscriptionStart { get; init; }

    [Required(ErrorMessage = "Не указана дата окончания абонемента")]
    /// <summary>Дата окончания абонемента.</summary>
    public DateOnly? SubscriptionEnd { get; init; }

    /// <inheritdoc/>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (SubscriptionEnd < SubscriptionStart)
            yield return new ValidationResult(
                "Дата окончания абонемента не может быть раньше даты начала.",
                [nameof(SubscriptionEnd)]);
    }
}
