using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Tests;

/// <summary>
/// Генератор детерминированного набора тестовых данных фитнес-клуба.
/// </summary>
internal static class DataSeeder
{
    /// <summary>Фиксированная дата «сейчас» для детерминированных тестов.</summary>
    public static readonly DateOnly Today = new(2025, 1, 15);

    /// <summary>Название зала А.</summary>
    public const string HallA = "Зал А";

    /// <summary>Название зала Б.</summary>
    public const string HallB = "Зал Б";

    /// <summary>Название зала В.</summary>
    public const string HallC = "Зал В";

    /// <summary>Название бассейна.</summary>
    public const string Pool = "Бассейн";

    /// <summary>Создаёт набор связанных тестовых данных фитнес-клуба.</summary>
    public static SeedResult Seed()
    {
        var specializations = CreateSpecializations();
        var trainers = CreateTrainers(specializations);
        var clients = CreateClients();
        var bookings = CreateBookings(clients, trainers);

        return new SeedResult(specializations, clients, trainers, bookings);
    }

    private static List<Specialization> CreateSpecializations() =>
    [
        new() { Name = "Силовой тренинг" },
        new() { Name = "Йога" },
        new() { Name = "Кроссфит" },
        new() { Name = "Пилатес" },
        new() { Name = "Бокс" },
        new() { Name = "Плавание" },
        new() { Name = "Аэробика" },
        new() { Name = "Стретчинг" },
        new() { Name = "Функциональный тренинг" },
        new() { Name = "Танцы" },
    ];

    private static List<Trainer> CreateTrainers(List<Specialization> specializations)
    {
        var spec = specializations.ToDictionary(s => s.Name, s => s.Id);

        return
        [
            new() { PassportNumber = "1000 000001", FirstName = "Андрей",   LastName = "Соколов",  Gender = Gender.Male,   BirthDate = new DateOnly(1985,  4, 12), SpecializationId = spec["Силовой тренинг"],        ExperienceYears = 12 },
            new() { PassportNumber = "1000 000002", FirstName = "Наталья",  LastName = "Орлова",   Gender = Gender.Female, BirthDate = new DateOnly(1990,  7, 25), SpecializationId = spec["Йога"],                   ExperienceYears = 8  },
            new() { PassportNumber = "1000 000003", FirstName = "Виктор",   LastName = "Захаров",  Gender = Gender.Male,   BirthDate = new DateOnly(1980,  1,  9), SpecializationId = spec["Кроссфит"],               ExperienceYears = 15 },
            new() { PassportNumber = "1000 000004", FirstName = "Юлия",     LastName = "Лебедева", Gender = Gender.Female, BirthDate = new DateOnly(1993, 11,  3), SpecializationId = spec["Пилатес"],                ExperienceYears = 7  },
            new() { PassportNumber = "1000 000005", FirstName = "Максим",   LastName = "Козлов",   Gender = Gender.Male,   BirthDate = new DateOnly(1988,  6, 18), SpecializationId = spec["Бокс"],                   ExperienceYears = 10 },
            new() { PassportNumber = "1000 000006", FirstName = "Ирина",    LastName = "Павлова",  Gender = Gender.Female, BirthDate = new DateOnly(1995,  2, 22), SpecializationId = spec["Йога"],                   ExperienceYears = 4  },
            new() { PassportNumber = "1000 000007", FirstName = "Роман",    LastName = "Никитин",  Gender = Gender.Male,   BirthDate = new DateOnly(1982,  9, 14), SpecializationId = spec["Плавание"],               ExperienceYears = 11 },
            new() { PassportNumber = "1000 000008", FirstName = "Светлана", LastName = "Гусева",   Gender = Gender.Female, BirthDate = new DateOnly(1998,  3, 30), SpecializationId = spec["Стретчинг"],              ExperienceYears = 3  },
            new() { PassportNumber = "1000 000009", FirstName = "Артём",    LastName = "Титов",    Gender = Gender.Male,   BirthDate = new DateOnly(1991,  5, 12), SpecializationId = spec["Функциональный тренинг"], ExperienceYears = 6  },
            new() { PassportNumber = "1000 000010", FirstName = "Ксения",   LastName = "Белова",   Gender = Gender.Female, BirthDate = new DateOnly(1997, 12,  7), SpecializationId = spec["Танцы"],                  ExperienceYears = 5  },
            new() { PassportNumber = "1000 000011", FirstName = "Олег",     LastName = "Дмитриев", Gender = Gender.Male,   BirthDate = new DateOnly(1979,  8, 21), SpecializationId = spec["Бокс"],                   ExperienceYears = 16 },
            new() { PassportNumber = "1000 000012", FirstName = "Вера",     LastName = "Крылова",  Gender = Gender.Female, BirthDate = new DateOnly(1994, 10,  1), SpecializationId = spec["Аэробика"],               ExperienceYears = 2  },
        ];
    }

    private static List<Client> CreateClients() =>
    [
        new() { PassportNumber = "2000 000001", FirstName = "Иван",    LastName = "Иванов",    Gender = Gender.Male,   BirthDate = new DateOnly(1990,  5, 12), Phone = "+7-900-100-10-01", MembershipStartDate = new DateOnly(2023,  1,  1), MembershipEndDate = new DateOnly(2024,  1,  1) },
        new() { PassportNumber = "2000 000002", FirstName = "Алексей", LastName = "Смирнов",   Gender = Gender.Male,   BirthDate = new DateOnly(1985,  8,  3), Phone = "+7-900-100-10-02", MembershipStartDate = new DateOnly(2023,  6,  1), MembershipEndDate = new DateOnly(2024,  6,  1) },
        new() { PassportNumber = "2000 000003", FirstName = "Дмитрий", LastName = "Попов",     Gender = Gender.Male,   BirthDate = new DateOnly(1988,  3, 30), Phone = "+7-900-100-10-03", MembershipStartDate = new DateOnly(2023,  9,  1), MembershipEndDate = new DateOnly(2024,  9,  1) },
        new() { PassportNumber = "2000 000004", FirstName = "Ольга",   LastName = "Морозова",  Gender = Gender.Female, BirthDate = new DateOnly(1993, 12, 25), Phone = "+7-900-100-10-04", MembershipStartDate = new DateOnly(2024,  1,  1), MembershipEndDate = new DateOnly(2024, 12,  1) },
        new() { PassportNumber = "2000 000005", FirstName = "Сергей",  LastName = "Новиков",   Gender = Gender.Male,   BirthDate = new DateOnly(1980,  1, 18), Phone = "+7-900-100-10-05", MembershipStartDate = new DateOnly(2023, 10,  1), MembershipEndDate = new DateOnly(2024, 10,  1) },
        new() { PassportNumber = "2000 000006", FirstName = "Пётр",    LastName = "Петров",    Gender = Gender.Male,   BirthDate = new DateOnly(1992,  7, 15), Phone = "+7-900-100-10-06", MembershipStartDate = new DateOnly(2024,  9,  1), MembershipEndDate = new DateOnly(2025,  9,  1) },
        new() { PassportNumber = "2000 000007", FirstName = "Анна",    LastName = "Сидорова",  Gender = Gender.Female, BirthDate = new DateOnly(1995,  2, 20), Phone = "+7-900-100-10-07", MembershipStartDate = new DateOnly(2024,  5, 15), MembershipEndDate = new DateOnly(2025,  5, 15) },
        new() { PassportNumber = "2000 000008", FirstName = "Мария",   LastName = "Кузнецова", Gender = Gender.Female, BirthDate = new DateOnly(2000, 11,  1), Phone = "+7-900-100-10-08", MembershipStartDate = new DateOnly(2024,  7, 15), MembershipEndDate = new DateOnly(2025,  7, 15) },
        new() { PassportNumber = "2000 000009", FirstName = "Елена",   LastName = "Волкова",   Gender = Gender.Female, BirthDate = new DateOnly(1997,  9,  9), Phone = "+7-900-100-10-09", MembershipStartDate = new DateOnly(2024, 11, 15), MembershipEndDate = new DateOnly(2025, 11, 15) },
        new() { PassportNumber = "2000 000010", FirstName = "Татьяна", LastName = "Фёдорова",  Gender = Gender.Female, BirthDate = new DateOnly(1999,  6,  6), Phone = "+7-900-100-10-10", MembershipStartDate = new DateOnly(2024, 12, 31), MembershipEndDate = new DateOnly(2025, 12, 31) },
        new() { PassportNumber = "2000 000011", FirstName = "Николай", LastName = "Егоров",    Gender = Gender.Male,   BirthDate = new DateOnly(1991,  4, 22), Phone = "+7-900-100-10-11", MembershipStartDate = new DateOnly(2023,  3,  1), MembershipEndDate = new DateOnly(2024,  3,  1) },
        new() { PassportNumber = "2000 000012", FirstName = "Юлия",    LastName = "Тарасова",  Gender = Gender.Female, BirthDate = new DateOnly(1996, 10, 11), Phone = "+7-900-100-10-12", MembershipStartDate = new DateOnly(2024,  6, 10), MembershipEndDate = new DateOnly(2025,  6, 10) },
    ];

    private static List<Booking> CreateBookings(List<Client> clients, List<Trainer> trainers)
    {
        var assignment = new (int trainerIndex, string hall, DateTime at, bool trial)[]
        {
            (0, HallA, new DateTime(2025, 1,  5, 10, 0, 0, DateTimeKind.Utc), false),
            (0, HallB, new DateTime(2025, 1, 10, 11, 0, 0, DateTimeKind.Utc), false),
            (0, HallA, new DateTime(2025, 1, 15, 14, 0, 0, DateTimeKind.Utc), true),
            (0, HallC, new DateTime(2025, 1, 20, 15, 0, 0, DateTimeKind.Utc), false),
            (0, HallA, new DateTime(2025, 2,  5, 10, 0, 0, DateTimeKind.Utc), false),
            (1, HallC, new DateTime(2025, 1,  7,  9, 0, 0, DateTimeKind.Utc), false),
            (1, Pool,  new DateTime(2025, 1, 12, 10, 0, 0, DateTimeKind.Utc), true),
            (1, HallC, new DateTime(2025, 1, 18, 11, 0, 0, DateTimeKind.Utc), false),
            (1, HallC, new DateTime(2024, 12, 10, 10, 0, 0, DateTimeKind.Utc), false),
            (2, HallA, new DateTime(2025, 1,  8, 16, 0, 0, DateTimeKind.Utc), false),
            (2, HallB, new DateTime(2025, 1, 15, 17, 0, 0, DateTimeKind.Utc), false),
            (2, HallA, new DateTime(2025, 1, 22, 18, 0, 0, DateTimeKind.Utc), false),
            (3, HallC, new DateTime(2025, 1, 11, 12, 0, 0, DateTimeKind.Utc), false),
            (3, Pool,  new DateTime(2025, 1, 17, 13, 0, 0, DateTimeKind.Utc), false),
            (4, HallA, new DateTime(2025, 1, 14, 19, 0, 0, DateTimeKind.Utc), true),
            (5, HallC, new DateTime(2025, 1, 16,  9, 0, 0, DateTimeKind.Utc), false),
        };

        var result = new List<Booking>();
        for (var i = 0; i < assignment.Length; i++)
        {
            var (trainerIndex, hall, at, trial) = assignment[i];
            result.Add(new Booking
            {
                ClientId    = clients[i % clients.Count].Id,
                TrainerId   = trainers[trainerIndex].Id,
                HallName    = hall,
                ScheduledAt = at,
                IsTrial     = trial,
            });
        }

        return result;
    }
}

/// <summary>
/// Результат генерации набора тестовых данных фитнес-клуба.
/// </summary>
internal record SeedResult(
    List<Specialization> Specializations,
    List<Client> Clients,
    List<Trainer> Trainers,
    List<Booking> Bookings);
