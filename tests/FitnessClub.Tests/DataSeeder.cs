using Bogus;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Tests;

/// <summary>
/// Генератор тестовых данных для доменной модели финтес-клуба.
/// </summary>
internal static class DataSeeder
{
    /// <summary>Число сущностей каждого вида, создаваемых по умолчанию.</summary>
    public const int DefaultCount = 12;

    /// <summary>Фиксированная дата «сейчас» для детерминированных тестов.</summary>
    public static readonly DateOnly Today = new(2025, 1, 15);

    /// <summary>
    /// Создаёт набор связанных тестовых данных финтес-клуба.
    /// </summary>
    /// <param name="count">Количество сущностей каждого вида (минимум 10).</param>
    public static SeedResult Seed(int count = DefaultCount)
    {
        Randomizer.Seed = new Random(42);

        var trainers = CreateTrainers(count);
        var clients = CreateClients(count);
        var memberships = CreateMemberships(clients);
        var workouts = CreateWorkouts(trainers, count);
        var visits = CreateVisits(clients, workouts);

        return new SeedResult(clients, memberships, trainers, workouts, visits);
    }

    private static List<Trainer> CreateTrainers(int count)
    {
        var faker = new Faker<Trainer>()
            .CustomInstantiator(f => new Trainer
            {
                FirstName = f.Name.FirstName(),
                LastName = f.Name.LastName(),
                Specialization = f.PickRandom(
                    "Силовой тренинг", "Йога", "Кроссфит", "Пилатес",
                    "Бокс", "Аэробика", "Плавание", "Стретчинг",
                    "Функциональный тренинг", "Танцы"),
                ExperienceYears = f.Random.Int(1, 20),
            });

        return faker.Generate(count);
    }

    private static List<Client> CreateClients(int count)
    {
        var faker = new Faker<Client>()
            .CustomInstantiator(f => new Client
            {
                FirstName = f.Name.FirstName(),
                LastName = f.Name.LastName(),
                Phone = f.Phone.PhoneNumber("+7-9##-###-##-##"),
                Email = f.Internet.Email(),
                BirthDate = DateOnly.FromDateTime(
                    f.Date.Past(45, DateTime.UtcNow.AddYears(-18))),
                RegistrationDate = DateOnly.FromDateTime(
                    f.Date.Past(3, DateTime.UtcNow)),
            });

        return faker.Generate(count);
    }

    private static List<Membership> CreateMemberships(List<Client> clients)
    {
        var faker = new Faker<Membership>()
            .CustomInstantiator(f =>
            {
                var client = f.PickRandom(clients);
                var type = f.PickRandom<MembershipType>();
                var start = Today.AddDays(-f.Random.Int(1, 365));
                var length = type switch
                {
                    MembershipType.Monthly => 30,
                    MembershipType.Quarterly => 90,
                    _ => 365,
                };
                var price = type switch
                {
                    MembershipType.Monthly => 2500m,
                    MembershipType.Quarterly => 7000m,
                    _ => 24000m,
                };

                return new Membership
                {
                    ClientId = client.Id,
                    Type = type,
                    StartDate = start,
                    EndDate = start.AddDays(length),
                    Price = price,
                };
            });

        return faker.Generate(clients.Count);
    }

    private static List<Workout> CreateWorkouts(List<Trainer> trainers, int count)
    {
        var faker = new Faker<Workout>()
            .CustomInstantiator(f => new Workout
            {
                Name = f.PickRandom(
                    "Утренняя йога", "Силовая тренировка", "Кроссфит интенсив",
                    "Пилатес для начинающих", "Бокс: спарринг", "Аэробика",
                    "Плавание", "Стретчинг вечерний",
                    "Функциональный тренинг", "Танцевальный микс"),
                TrainerId = f.PickRandom(trainers).Id,
                ScheduledAt = DateTime.UtcNow.AddDays(f.Random.Int(1, 30)),
                Duration = TimeSpan.FromMinutes(f.PickRandom(45, 60, 90)),
                MaxParticipants = f.Random.Int(8, 25),
                Price = f.PickRandom(500m, 600m, 700m, 800m, 900m, 1000m),
            });

        return faker.Generate(count);
    }

    private static List<Visit> CreateVisits(List<Client> clients, List<Workout> workouts)
    {
        var faker = new Faker<Visit>()
            .CustomInstantiator(f => new Visit
            {
                ClientId = f.PickRandom(clients).Id,
                WorkoutId = f.PickRandom(workouts).Id,
                VisitTime = DateTime.UtcNow.AddDays(-f.Random.Int(1, 60)),
                Attended = f.Random.Bool(0.85f),
            });

        return faker.Generate(clients.Count);
    }
}

/// <summary>
/// Результат генерации набора тестовых данных финтес-клуба.
/// </summary>
internal record SeedResult(
    List<Client> Clients,
    List<Membership> Memberships,
    List<Trainer> Trainers,
    List<Workout> Workouts,
    List<Visit> Visits);
