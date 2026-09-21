using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Tests;

/// <summary>
/// Генератор детерминированного набора тестовых данных финтес-клуба.
/// </summary>
internal static class DataSeeder
{
    /// <summary>Фиксированная дата «сейчас» для детерминированных тестов.</summary>
    public static readonly DateOnly Today = new(2025, 1, 15);

    /// <summary>
    /// Создаёт набор связанных тестовых данных финтес-клуба.
    /// </summary>
    public static SeedResult Seed()
    {
        var trainers = CreateTrainers();
        var clients = CreateClients();
        var memberships = CreateMemberships(clients);
        var workouts = CreateWorkouts(trainers);
        var visits = CreateVisits(clients, workouts);

        return new SeedResult(clients, memberships, trainers, workouts, visits);
    }

    private static List<Trainer> CreateTrainers() =>
    [
        new() { FirstName = "Андрей",   LastName = "Соколов",  Specialization = "Силовой тренинг",        ExperienceYears = 8  },
        new() { FirstName = "Наталья",  LastName = "Орлова",   Specialization = "Йога",                   ExperienceYears = 5  },
        new() { FirstName = "Виктор",   LastName = "Захаров",  Specialization = "Кроссфит",               ExperienceYears = 10 },
        new() { FirstName = "Юлия",     LastName = "Лебедева", Specialization = "Пилатес",                ExperienceYears = 6  },
        new() { FirstName = "Максим",   LastName = "Козлов",   Specialization = "Бокс",                   ExperienceYears = 12 },
        new() { FirstName = "Ирина",    LastName = "Павлова",  Specialization = "Аэробика",               ExperienceYears = 4  },
        new() { FirstName = "Роман",    LastName = "Никитин",  Specialization = "Плавание",               ExperienceYears = 9  },
        new() { FirstName = "Светлана", LastName = "Гусева",   Specialization = "Стретчинг",              ExperienceYears = 3  },
        new() { FirstName = "Артём",    LastName = "Титов",    Specialization = "Функциональный тренинг", ExperienceYears = 7  },
        new() { FirstName = "Ксения",   LastName = "Белова",   Specialization = "Танцы",                  ExperienceYears = 5  },
        new() { FirstName = "Олег",     LastName = "Дмитриев", Specialization = "Кроссфит",               ExperienceYears = 11 },
        new() { FirstName = "Вера",     LastName = "Крылова",  Specialization = "Йога",                   ExperienceYears = 6  },
    ];

    private static List<Client> CreateClients() =>
    [
        new() { FirstName = "Иван",    LastName = "Иванов",    Phone = "+7-900-100-10-01", Email = "ivanov@mail.ru",    BirthDate = new DateOnly(1990,  5, 12), RegistrationDate = new DateOnly(2023,  1, 15) },
        new() { FirstName = "Пётр",    LastName = "Петров",    Phone = "+7-900-100-10-02", Email = "petrov@mail.ru",    BirthDate = new DateOnly(1985,  8,  3), RegistrationDate = new DateOnly(2024,  1, 15) },
        new() { FirstName = "Анна",    LastName = "Сидорова",  Phone = "+7-900-100-10-03", Email = "sidorova@mail.ru",  BirthDate = new DateOnly(1995,  2, 20), RegistrationDate = new DateOnly(2024,  5, 15) },
        new() { FirstName = "Мария",   LastName = "Кузнецова", Phone = "+7-900-100-10-04", Email = "kuznetsova@mail.ru",BirthDate = new DateOnly(2000, 11,  1), RegistrationDate = new DateOnly(2024,  7, 15) },
        new() { FirstName = "Алексей", LastName = "Смирнов",   Phone = "+7-900-100-10-05", Email = "smirnov@mail.ru",   BirthDate = new DateOnly(1992,  7, 15), RegistrationDate = new DateOnly(2024,  9, 15) },
        new() { FirstName = "Дмитрий", LastName = "Попов",     Phone = "+7-900-100-10-06", Email = "popov@mail.ru",     BirthDate = new DateOnly(1988,  3, 30), RegistrationDate = new DateOnly(2024, 10, 15) },
        new() { FirstName = "Елена",   LastName = "Волкова",   Phone = "+7-900-100-10-07", Email = "volkova@mail.ru",   BirthDate = new DateOnly(1997,  9,  9), RegistrationDate = new DateOnly(2024, 11, 15) },
        new() { FirstName = "Ольга",   LastName = "Морозова",  Phone = "+7-900-100-10-08", Email = "morozova@mail.ru",  BirthDate = new DateOnly(1993, 12, 25), RegistrationDate = new DateOnly(2024, 11, 15) },
        new() { FirstName = "Сергей",  LastName = "Новиков",   Phone = "+7-900-100-10-09", Email = "novikov@mail.ru",   BirthDate = new DateOnly(1980,  1, 18), RegistrationDate = new DateOnly(2024, 12, 15) },
        new() { FirstName = "Татьяна", LastName = "Фёдорова",  Phone = "+7-900-100-10-10", Email = "fedorova@mail.ru",  BirthDate = new DateOnly(1999,  6,  6), RegistrationDate = new DateOnly(2024, 12, 31) },
        new() { FirstName = "Николай", LastName = "Егоров",    Phone = "+7-900-100-10-11", Email = "egorov@mail.ru",    BirthDate = new DateOnly(1991,  4, 22), RegistrationDate = new DateOnly(2025,  1,  1) },
        new() { FirstName = "Юлия",    LastName = "Тарасова",  Phone = "+7-900-100-10-12", Email = "tarasova@mail.ru",  BirthDate = new DateOnly(1996, 10, 11), RegistrationDate = new DateOnly(2025,  1, 10) },
    ];

    private static List<Membership> CreateMemberships(List<Client> clients)
    {
        var types = new[]
        {
            MembershipType.Yearly,    MembershipType.Monthly,   MembershipType.Quarterly,
            MembershipType.Yearly,    MembershipType.Monthly,   MembershipType.Quarterly,
            MembershipType.Monthly,   MembershipType.Yearly,    MembershipType.Monthly,
            MembershipType.Quarterly, MembershipType.Yearly,    MembershipType.Monthly,
        };

        var result = new List<Membership>();
        for (var i = 0; i < clients.Count; i++)
        {
            var type = types[i];
            var start = Today.AddDays(-(i + 1) * 20);
            var length = type switch
            {
                MembershipType.Monthly   => 30,
                MembershipType.Quarterly => 90,
                _                        => 365,
            };
            var price = type switch
            {
                MembershipType.Monthly   => 2500m,
                MembershipType.Quarterly => 7000m,
                _                        => 24000m,
            };

            result.Add(new Membership
            {
                ClientId  = clients[i].Id,
                Type      = type,
                StartDate = start,
                EndDate   = start.AddDays(length),
                Price     = price,
            });
        }

        return result;
    }

    private static List<Workout> CreateWorkouts(List<Trainer> trainers) =>
    [
        new() { Name = "Утренняя йога",           TrainerId = trainers[1].Id,  ScheduledAt = new DateTime(2025, 1, 16,  8, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1),    MaxParticipants = 15, Price = 500m  },
        new() { Name = "Силовая тренировка",      TrainerId = trainers[0].Id,  ScheduledAt = new DateTime(2025, 1, 16, 18, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromMinutes(90), MaxParticipants = 12, Price = 800m  },
        new() { Name = "Кроссфит интенсив",       TrainerId = trainers[2].Id,  ScheduledAt = new DateTime(2025, 1, 17, 19, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1),    MaxParticipants = 10, Price = 900m  },
        new() { Name = "Пилатес для начинающих",  TrainerId = trainers[3].Id,  ScheduledAt = new DateTime(2025, 1, 17, 10, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1),    MaxParticipants = 20, Price = 600m  },
        new() { Name = "Бокс: спарринг",          TrainerId = trainers[4].Id,  ScheduledAt = new DateTime(2025, 1, 18, 20, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromMinutes(90), MaxParticipants = 8,  Price = 1000m },
        new() { Name = "Аэробика",                TrainerId = trainers[5].Id,  ScheduledAt = new DateTime(2025, 1, 18,  9, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1),    MaxParticipants = 25, Price = 500m  },
        new() { Name = "Плавание",                TrainerId = trainers[6].Id,  ScheduledAt = new DateTime(2025, 1, 19,  7, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1),    MaxParticipants = 10, Price = 700m  },
        new() { Name = "Стретчинг вечерний",      TrainerId = trainers[7].Id,  ScheduledAt = new DateTime(2025, 1, 19, 21, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1),    MaxParticipants = 18, Price = 550m  },
        new() { Name = "Функциональный тренинг",  TrainerId = trainers[8].Id,  ScheduledAt = new DateTime(2025, 1, 20, 18, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1),    MaxParticipants = 12, Price = 850m  },
        new() { Name = "Танцевальный микс",       TrainerId = trainers[9].Id,  ScheduledAt = new DateTime(2025, 1, 20, 19, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromMinutes(90), MaxParticipants = 20, Price = 650m  },
        new() { Name = "Силовая для продвинутых", TrainerId = trainers[10].Id, ScheduledAt = new DateTime(2025, 1, 21, 18, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromMinutes(90), MaxParticipants = 10, Price = 950m  },
        new() { Name = "Йога для беременных",     TrainerId = trainers[11].Id, ScheduledAt = new DateTime(2025, 1, 21, 11, 0, 0, DateTimeKind.Utc), Duration = TimeSpan.FromHours(1),    MaxParticipants = 8,  Price = 700m  },
    ];

    private static List<Visit> CreateVisits(List<Client> clients, List<Workout> workouts)
    {
        var attendedFlags = new[] { true, true, true, false, true, true, true, false, true, true, true, true };

        var result = new List<Visit>();
        for (var i = 0; i < clients.Count; i++)
        {
            result.Add(new Visit
            {
                ClientId  = clients[i].Id,
                WorkoutId = workouts[i].Id,
                VisitTime = new DateTime(2025, 1, 5, 12, 0, 0, DateTimeKind.Utc).AddDays(i),
                Attended  = attendedFlags[i],
            });
        }

        return result;
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