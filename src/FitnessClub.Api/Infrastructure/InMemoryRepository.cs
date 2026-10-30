using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Api.Infrastructure;

public class InMemoryRepository
{
    private readonly object _lock = new();
    public List<Specialization> Specializations { get; } = [];
    public List<Client> Clients { get; } = [];
    public List<Trainer> Trainers { get; } = [];
    public List<Booking> Bookings { get; } = [];

    public InMemoryRepository()
    {
        SeedData();
    }

    private void SeedData()
    {
        var spec1 = new Specialization { Name = "Силовой тренинг" };
        var spec2 = new Specialization { Name = "Йога" };
        var spec3 = new Specialization { Name = "Кроссфит" };
        Specializations.AddRange([spec1, spec2, spec3]);

        var t1 = new Trainer { FirstName = "Андрей", LastName = "Соколов", Gender = Gender.Male, BirthDate = new DateOnly(1985, 4, 12), SpecializationId = spec1.Id, ExperienceYears = 12, PassportNumber = "1000 000001" };
        var t2 = new Trainer { FirstName = "Наталья", LastName = "Орлова", Gender = Gender.Female, BirthDate = new DateOnly(1990, 7, 25), SpecializationId = spec2.Id, ExperienceYears = 8, PassportNumber = "1000 000002" };
        var t3 = new Trainer { FirstName = "Виктор", LastName = "Захаров", Gender = Gender.Male, BirthDate = new DateOnly(1980, 1, 9), SpecializationId = spec3.Id, ExperienceYears = 3, PassportNumber = "1000 000003" };
        Trainers.AddRange([t1, t2, t3]);

        var c1 = new Client { FirstName = "Иван", LastName = "Иванов", Gender = Gender.Male, BirthDate = new DateOnly(1990, 5, 12), Phone = "+7-900-100-10-01", MembershipStartDate = new DateOnly(2023, 1, 1), MembershipEndDate = new DateOnly(2024, 1, 1), PassportNumber = "2000 000001" };
        var c2 = new Client { FirstName = "Анна", LastName = "Сидорова", Gender = Gender.Female, BirthDate = new DateOnly(1995, 2, 20), Phone = "+7-900-100-10-02", MembershipStartDate = new DateOnly(2024, 1, 1), MembershipEndDate = new DateOnly(2025, 12, 31), PassportNumber = "2000 000002" };
        Clients.AddRange([c1, c2]);

        Bookings.AddRange([
            new Booking { ClientId = c2.Id, TrainerId = t1.Id, HallName = "Зал А", ScheduledAt = DateTime.UtcNow.AddDays(1), IsTrial = false },
            new Booking { ClientId = c1.Id, TrainerId = t2.Id, HallName = "Зал Б", ScheduledAt = DateTime.UtcNow.AddDays(2), IsTrial = true }
        ]);
    }

    public List<Client> GetClients() { lock (_lock) return Clients.ToList(); }
    public Client? GetClient(Guid id) { lock (_lock) return Clients.FirstOrDefault(c => c.Id == id); }
    public void AddClient(Client client) { lock (_lock) Clients.Add(client); }
    public bool DeleteClient(Guid id) { lock (_lock) { var c = GetClient(id); if (c == null) return false; Clients.Remove(c); return true; } }

    public List<Trainer> GetTrainers() { lock (_lock) return Trainers.ToList(); }
    public void AddTrainer(Trainer trainer) { lock (_lock) Trainers.Add(trainer); }

    public List<Booking> GetBookings() { lock (_lock) return Bookings.ToList(); }
    public void AddBooking(Booking booking) { lock (_lock) Bookings.Add(booking); }
}