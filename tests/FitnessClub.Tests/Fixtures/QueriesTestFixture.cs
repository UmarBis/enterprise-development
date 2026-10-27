using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Tests;

/// <summary>
/// Фикстура, содержащая тестовые данные фитнес-клуба.
/// </summary>
public class QueriesTestFixture
{
    /// <summary>
    /// Момент времени, относительно которого построены тестовые данные.
    /// </summary>
    public DateTime Now { get; }

    /// <summary>
    /// Дата, относительно которой построены тестовые данные.
    /// </summary>
    public DateOnly Today { get; }

    /// <summary>
    /// Справочник специализаций тренеров.
    /// </summary>
    public readonly List<Specialization> Specializations;

    /// <summary>
    /// Клиенты фитнес-клуба.
    /// </summary>
    public readonly List<Client> Clients;

    /// <summary>
    /// Тренеры фитнес-клуба.
    /// </summary>
    public readonly List<Trainer> Trainers;

    /// <summary>
    /// Записи клиентов на персональные занятия.
    /// </summary>
    public readonly List<Booking> Bookings;

    /// <summary>
    /// Инициализирует тестовые данные.
    /// </summary>
    public QueriesTestFixture()
    {
        // Жестко фиксируем время: 15 января 2025 года, 14:00 UTC
        Now = new DateTime(2025, 1, 15, 14, 0, 0, DateTimeKind.Utc);
        Today = new DateOnly(2025, 1, 15);

        Specializations =
        [
            new Specialization { Name = "Силовой тренинг" },
            new Specialization { Name = "Йога" },
            new Specialization { Name = "Кроссфит" },
            new Specialization { Name = "Пилатес" },
            new Specialization { Name = "Бокс" },
            new Specialization { Name = "Плавание" },
            new Specialization { Name = "Аэробика" },
            new Specialization { Name = "Стретчинг" },
            new Specialization { Name = "Функциональный тренинг" },
            new Specialization { Name = "Танцы" },
        ];

        Clients =
        [
            // Клиенты с просроченным абонементом (EndDate < 15.01.2025)
            new Client { FirstName = "Иван", LastName = "Иванов", Gender = Gender.Male, BirthDate = new DateOnly(1990, 5, 12), Phone = "+7-900-100-10-01", MembershipStartDate = new DateOnly(2023, 1, 1), MembershipEndDate = new DateOnly(2024, 1, 1), PassportNumber = "2000 000001" },
            new Client { FirstName = "Алексей", LastName = "Смирнов", Gender = Gender.Male, BirthDate = new DateOnly(1985, 8, 3), Phone = "+7-900-100-10-02", MembershipStartDate = new DateOnly(2023, 6, 1), MembershipEndDate = new DateOnly(2024, 6, 1), PassportNumber = "2000 000002" },
            new Client { FirstName = "Дмитрий", LastName = "Попов", Gender = Gender.Male, BirthDate = new DateOnly(1988, 3, 30), Phone = "+7-900-100-10-03", MembershipStartDate = new DateOnly(2023, 9, 1), MembershipEndDate = new DateOnly(2024, 9, 1), PassportNumber = "2000 000003" },
            new Client { FirstName = "Ольга", LastName = "Морозова", Gender = Gender.Female, BirthDate = new DateOnly(1993, 12, 25), Phone = "+7-900-100-10-04", MembershipStartDate = new DateOnly(2024, 1, 1), MembershipEndDate = new DateOnly(2024, 12, 1), PassportNumber = "2000 000004" },
            new Client { FirstName = "Сергей", LastName = "Новиков", Gender = Gender.Male, BirthDate = new DateOnly(1980, 1, 18), Phone = "+7-900-100-10-05", MembershipStartDate = new DateOnly(2023, 10, 1), MembershipEndDate = new DateOnly(2024, 10, 1), PassportNumber = "2000 000005" },
            new Client { FirstName = "Николай", LastName = "Егоров", Gender = Gender.Male, BirthDate = new DateOnly(1991, 4, 22), Phone = "+7-900-100-10-06", MembershipStartDate = new DateOnly(2023, 3, 1), MembershipEndDate = new DateOnly(2024, 3, 1), PassportNumber = "2000 000011" },
            
            // Клиенты с действующим абонементом
            new Client { FirstName = "Пётр", LastName = "Петров", Gender = Gender.Male, BirthDate = new DateOnly(1992, 7, 15), Phone = "+7-900-100-10-07", MembershipStartDate = new DateOnly(2024, 9, 1), MembershipEndDate = new DateOnly(2025, 9, 1), PassportNumber = "2000 000006" },
            new Client { FirstName = "Анна", LastName = "Сидорова", Gender = Gender.Female, BirthDate = new DateOnly(1995, 2, 20), Phone = "+7-900-100-10-08", MembershipStartDate = new DateOnly(2024, 5, 15), MembershipEndDate = new DateOnly(2025, 5, 15), PassportNumber = "2000 000007" },
            new Client { FirstName = "Мария", LastName = "Кузнецова", Gender = Gender.Female, BirthDate = new DateOnly(2000, 11, 1), Phone = "+7-900-100-10-09", MembershipStartDate = new DateOnly(2024, 7, 15), MembershipEndDate = new DateOnly(2025, 7, 15), PassportNumber = "2000 000008" },
            new Client { FirstName = "Елена", LastName = "Волкова", Gender = Gender.Female, BirthDate = new DateOnly(1997, 9, 9), Phone = "+7-900-100-10-10", MembershipStartDate = new DateOnly(2024, 11, 15), MembershipEndDate = new DateOnly(2025, 11, 15), PassportNumber = "2000 000009" },
            new Client { FirstName = "Татьяна", LastName = "Фёдорова", Gender = Gender.Female, BirthDate = new DateOnly(1999, 6, 6), Phone = "+7-900-100-10-11", MembershipStartDate = new DateOnly(2024, 12, 31), MembershipEndDate = new DateOnly(2025, 12, 31), PassportNumber = "2000 000010" },
            new Client { FirstName = "Юлия", LastName = "Тарасова", Gender = Gender.Female, BirthDate = new DateOnly(1996, 10, 11), Phone = "+7-900-100-10-12", MembershipStartDate = new DateOnly(2024, 6, 10), MembershipEndDate = new DateOnly(2025, 6, 10), PassportNumber = "2000 000012" },
        ];

        Trainers =
        [
            new Trainer { FirstName = "Андрей", LastName = "Соколов", Gender = Gender.Male, BirthDate = new DateOnly(1985, 4, 12), SpecializationId = Specializations[0].Id, ExperienceYears = 12, PassportNumber = "1000 000001" },
            new Trainer { FirstName = "Наталья", LastName = "Орлова", Gender = Gender.Female, BirthDate = new DateOnly(1990, 7, 25), SpecializationId = Specializations[1].Id, ExperienceYears = 8, PassportNumber = "1000 000002" },
            new Trainer { FirstName = "Виктор", LastName = "Захаров", Gender = Gender.Male, BirthDate = new DateOnly(1980, 1, 9), SpecializationId = Specializations[2].Id, ExperienceYears = 15, PassportNumber = "1000 000003" },
            new Trainer { FirstName = "Юлия", LastName = "Лебедева", Gender = Gender.Female, BirthDate = new DateOnly(1993, 11, 3), SpecializationId = Specializations[3].Id, ExperienceYears = 7, PassportNumber = "1000 000004" },
            new Trainer { FirstName = "Максим", LastName = "Козлов", Gender = Gender.Male, BirthDate = new DateOnly(1988, 6, 18), SpecializationId = Specializations[4].Id, ExperienceYears = 10, PassportNumber = "1000 000005" },
            new Trainer { FirstName = "Ирина", LastName = "Павлова", Gender = Gender.Female, BirthDate = new DateOnly(1995, 2, 22), SpecializationId = Specializations[1].Id, ExperienceYears = 4, PassportNumber = "1000 000006" },
            new Trainer { FirstName = "Роман", LastName = "Никитин", Gender = Gender.Male, BirthDate = new DateOnly(1982, 9, 14), SpecializationId = Specializations[5].Id, ExperienceYears = 11, PassportNumber = "1000 000007" },
            new Trainer { FirstName = "Светлана", LastName = "Гусева", Gender = Gender.Female, BirthDate = new DateOnly(1998, 3, 30), SpecializationId = Specializations[7].Id, ExperienceYears = 3, PassportNumber = "1000 000008" },
            new Trainer { FirstName = "Артём", LastName = "Титов", Gender = Gender.Male, BirthDate = new DateOnly(1991, 5, 12), SpecializationId = Specializations[8].Id, ExperienceYears = 6, PassportNumber = "1000 000009" },
            new Trainer { FirstName = "Ксения", LastName = "Белова", Gender = Gender.Female, BirthDate = new DateOnly(1997, 12, 7), SpecializationId = Specializations[9].Id, ExperienceYears = 5, PassportNumber = "1000 000010" },
            new Trainer { FirstName = "Олег", LastName = "Дмитриев", Gender = Gender.Male, BirthDate = new DateOnly(1979, 8, 21), SpecializationId = Specializations[4].Id, ExperienceYears = 16, PassportNumber = "1000 000011" },
            new Trainer { FirstName = "Вера", LastName = "Крылова", Gender = Gender.Female, BirthDate = new DateOnly(1994, 10, 1), SpecializationId = Specializations[6].Id, ExperienceYears = 2, PassportNumber = "1000 000012" },
        ];

        Bookings =
        [
            // Зал А (5 записей за январь 2025)
            new Booking { ClientId = Clients[6].Id, TrainerId = Trainers[0].Id, HallName = "Зал А", ScheduledAt = new DateTime(2025, 1, 5, 10, 0, 0, DateTimeKind.Utc), IsTrial = false },
            new Booking { ClientId = Clients[7].Id, TrainerId = Trainers[2].Id, HallName = "Зал А", ScheduledAt = new DateTime(2025, 1, 8, 16, 0, 0, DateTimeKind.Utc), IsTrial = false },
            new Booking { ClientId = Clients[8].Id, TrainerId = Trainers[4].Id, HallName = "Зал А", ScheduledAt = new DateTime(2025, 1, 14, 19, 0, 0, DateTimeKind.Utc), IsTrial = false },
            new Booking { ClientId = Clients[9].Id, TrainerId = Trainers[0].Id, HallName = "Зал А", ScheduledAt = new DateTime(2025, 1, 15, 14, 0, 0, DateTimeKind.Utc), IsTrial = true },
            new Booking { ClientId = Clients[10].Id, TrainerId = Trainers[2].Id, HallName = "Зал А", ScheduledAt = new DateTime(2025, 1, 22, 18, 0, 0, DateTimeKind.Utc), IsTrial = false },
            
            // Остальные записи для Соколова (всего 5)
            new Booking { ClientId = Clients[6].Id, TrainerId = Trainers[0].Id, HallName = "Зал Б", ScheduledAt = new DateTime(2025, 1, 10, 11, 0, 0, DateTimeKind.Utc), IsTrial = false },
            new Booking { ClientId = Clients[7].Id, TrainerId = Trainers[0].Id, HallName = "Зал В", ScheduledAt = new DateTime(2025, 1, 20, 15, 0, 0, DateTimeKind.Utc), IsTrial = false },
            new Booking { ClientId = Clients[8].Id, TrainerId = Trainers[0].Id, HallName = "Зал Б", ScheduledAt = new DateTime(2025, 2, 5, 10, 0, 0, DateTimeKind.Utc), IsTrial = false },
            
            // Орлова (всего 4)
            new Booking { ClientId = Clients[9].Id, TrainerId = Trainers[1].Id, HallName = "Зал В", ScheduledAt = new DateTime(2025, 1, 7, 9, 0, 0, DateTimeKind.Utc), IsTrial = false },
            new Booking { ClientId = Clients[10].Id, TrainerId = Trainers[1].Id, HallName = "Бассейн", ScheduledAt = new DateTime(2025, 1, 12, 10, 0, 0, DateTimeKind.Utc), IsTrial = true },
            new Booking { ClientId = Clients[11].Id, TrainerId = Trainers[1].Id, HallName = "Зал В", ScheduledAt = new DateTime(2025, 1, 18, 11, 0, 0, DateTimeKind.Utc), IsTrial = false },
            new Booking { ClientId = Clients[6].Id, TrainerId = Trainers[1].Id, HallName = "Зал В", ScheduledAt = new DateTime(2024, 12, 10, 10, 0, 0, DateTimeKind.Utc), IsTrial = false },
            
            // Захаров (всего 3)
            new Booking { ClientId = Clients[7].Id, TrainerId = Trainers[2].Id, HallName = "Зал Б", ScheduledAt = new DateTime(2025, 1, 15, 17, 0, 0, DateTimeKind.Utc), IsTrial = false },
            
            // Лебедева (всего 2)
            new Booking { ClientId = Clients[8].Id, TrainerId = Trainers[3].Id, HallName = "Зал В", ScheduledAt = new DateTime(2025, 1, 11, 12, 0, 0, DateTimeKind.Utc), IsTrial = false },
            new Booking { ClientId = Clients[9].Id, TrainerId = Trainers[3].Id, HallName = "Бассейн", ScheduledAt = new DateTime(2025, 1, 17, 13, 0, 0, DateTimeKind.Utc), IsTrial = false },
            
            // Павлова (всего 1)
            new Booking { ClientId = Clients[10].Id, TrainerId = Trainers[5].Id, HallName = "Зал В", ScheduledAt = new DateTime(2025, 1, 16, 9, 0, 0, DateTimeKind.Utc), IsTrial = false },
        ];
    }
}
