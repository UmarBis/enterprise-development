using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>DTO клиента фитнес-клуба: данные для создания и обновления.</summary>
public class UpsertClientDto : IValidatableObject
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

    /// <summary>Контактный телефон.</summary>
    [Required(ErrorMessage = "Не указан телефон")]
    [Phone(ErrorMessage = "Некорректный номер телефона")]
    public string Phone { get; init; } = string.Empty;

    /// <summary>Дата начала абонемента.</summary>
    [Required(ErrorMessage = "Не указана дата начала абонемента")]
    public DateOnly? SubscriptionStart { get; init; }

    /// <summary>Дата окончания абонемента.</summary>
    [Required(ErrorMessage = "Не указана дата окончания абонемента")]
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
