using FitnessClub.Contracts.Dto;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Application.Mapping;

public static class MappingExtensions
{
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

    public static SpecializationDto ToDto(this Specialization s) => new()
    {
        Id = s.Id,
        Name = s.Name
    };

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