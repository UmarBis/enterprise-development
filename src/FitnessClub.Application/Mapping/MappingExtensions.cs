using FitnessClub.Contracts.Dto;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Application.Mapping;

/// <summary>Содержит методы преобразования доменных сущностей в DTO.</summary>
public static class MappingExtensions
{
    /// <summary>Преобразует клиента в DTO.</summary>
    public static ClientDto ToDto(this Client c) => new()
    {
        Id = c.Id,
        PassportNumber = c.PassportNumber,
        FullName = c.FullName,
        Gender = c.Gender,
        BirthDate = c.BirthDate,
        Phone = c.Phone,
        SubscriptionStart = c.MembershipStartDate,
        SubscriptionEnd = c.MembershipEndDate
    };

    /// <summary>Преобразует тренера в DTO с названием специализации.</summary>
    public static TrainerDto ToDto(this Trainer t, string specializationName) => new()
    {
        Id = t.Id,
        PassportNumber = t.PassportNumber,
        FullName = t.FullName,
        Gender = t.Gender,
        BirthDate = t.BirthDate,
        SpecializationId = t.SpecializationId,
        SpecializationName = specializationName,
        ExperienceYears = t.ExperienceYears
    };

    /// <summary>Преобразует специализацию в DTO.</summary>
    public static SpecializationDto ToDto(this Specialization s) => new()
    {
        Id = s.Id,
        Name = s.Name
    };

    /// <summary>Преобразует запись на занятие в DTO.</summary>
    public static BookingDto ToDto(this Booking b) => new()
    {
        Id = b.Id,
        ClientId = b.ClientId,
        TrainerId = b.TrainerId,
        SessionDateTime = b.ScheduledAt,
        HallName = b.HallName,
        IsTrial = b.IsTrial
    };
}