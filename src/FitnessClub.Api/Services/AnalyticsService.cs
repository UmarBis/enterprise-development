using FitnessClub.Api.Infrastructure;
using FitnessClub.Domain.Entities;

namespace FitnessClub.Api.Services;

public class AnalyticsService(InMemoryRepository repo)
{
    public List<Trainer> GetTrainersWith5YearsExperience()
        => repo.GetTrainers().Where(t => t.ExperienceYears >= 5).OrderBy(t => t.FullName).ToList();

    public bool IsHallAvailable(string hallName, DateTime at, int durationMinutes)
    {
        var end = at.AddMinutes(durationMinutes);
        return !repo.GetBookings().Any(b => b.HallName == hallName && b.ScheduledAt < end && b.ScheduledAt.AddMinutes(durationMinutes) > at);
    }

    public List<Client> GetExpiredClients()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        return repo.GetClients().Where(c => c.IsMembershipExpired(today)).OrderBy(c => c.FullName).ToList();
    }

    public List<Booking> GetCurrentMonthBookings(string hallName)
    {
        var today = DateTime.UtcNow;
        return repo.GetBookings()
            .Where(b => b.HallName == hallName && b.ScheduledAt.Year == today.Year && b.ScheduledAt.Month == today.Month)
            .OrderBy(b => b.ScheduledAt).ToList();
    }

    public List<object> GetTop5Trainers()
    {
        var trainers = repo.GetTrainers().ToDictionary(t => t.Id, t => t.FullName);
        return repo.GetBookings()
            .GroupBy(b => b.TrainerId)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new { TrainerName = trainers.GetValueOrDefault(g.Key, "Неизвестно"), BookingsCount = g.Count() })
            .Cast<object>().ToList();
    }
}
