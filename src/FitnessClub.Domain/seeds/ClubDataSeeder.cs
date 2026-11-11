using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Domain.Seeds;

/// <summary>
/// Генератор набора данных фитнес-клуба: не менее 10 экземпляров каждого класса.
/// Даты абонементов и занятий вычисляются относительно переданного момента времени,
/// поэтому данные остаются пригодными независимо от времени запуска приложения.
/// </summary>
public static class ClubDataSeeder
{
    /// <summary>Детерминированный идентификатор по номеру (для воспроизводимых тестов).</summary>
    private static Guid G(int n) => Guid.Parse($"00000000-0000-0000-0000-{n:D12}");

    /// <summary>Длительность одного занятия (для проверки доступности зала).</summary>
    public static TimeSpan SessionDuration => TimeSpan.FromHours(1);

    /// <summary>
    /// Создаёт набор данных относительно указанного момента времени.
    /// </summary>
    /// <param name="now">Момент времени, относительно которого вычисляются даты;
    /// по умолчанию — текущий момент.</param>
    public static ClubData Create(DateTime? now = null)
    {
        var moment = now ?? DateTime.Now;
        var today = DateOnly.FromDateTime(moment);
        var currentMonth = new DateTime(moment.Year, moment.Month, 1);
        var previousMonth = currentMonth.AddMonths(-1);
        var nextMonth = currentMonth.AddMonths(1);

        static DateTime At(DateTime month, int day, int hour) =>
            new(month.Year, month.Month, day, hour, 0, 0);

        IReadOnlyList<Specialization> specializations =
        [
            new() { Id = G(0), Name = "Йога" },
            new() { Id = G(1), Name = "Пилатес" },
            new() { Id = G(2), Name = "Кроссфит" },
            new() { Id = G(3), Name = "Плавание" },
            new() { Id = G(4), Name = "Бокс" },
            new() { Id = G(5), Name = "Тяжёлая атлетика" },
            new() { Id = G(6), Name = "Стретчинг" },
            new() { Id = G(7), Name = "Функциональный тренинг" },
            new() { Id = G(8), Name = "Лёгкая атлетика" },
            new() { Id = G(9), Name = "Реабилитация" },
        ];

        IReadOnlyList<Client> clients =
        [
            new() { Id = G(100), PassportNumber = "4501 123456", FirstName = "Алексей", LastName = "Андреев", Gender = Gender.Male, BirthDate = new(1990, 4, 12), Phone = "+7 (911) 100-00-01", MembershipStartDate = today.AddDays(-60), MembershipEndDate = today.AddDays(-30) },
            new() { Id = G(101), PassportNumber = "4502 234567", FirstName = "Марина", LastName = "Борисова", Gender = Gender.Female, BirthDate = new(1985, 11, 25), Phone = "+7 (911) 100-00-02", MembershipStartDate = today.AddDays(-90), MembershipEndDate = today.AddDays(-7) },
            new() { Id = G(102), PassportNumber = "4503 345678", FirstName = "Дмитрий", LastName = "Васильев", Gender = Gender.Male, BirthDate = new(1978, 2, 18), Phone = "+7 (911) 100-00-03", MembershipStartDate = today.AddDays(-120), MembershipEndDate = today.AddDays(-1) },
            new() { Id = G(103), PassportNumber = "4504 456789", FirstName = "Елена", LastName = "Гаврилова", Gender = Gender.Female, BirthDate = new(1992, 6, 30), Phone = "+7 (911) 100-00-04", MembershipStartDate = today.AddDays(-445), MembershipEndDate = today.AddDays(-45) },
            new() { Id = G(104), PassportNumber = "4505 567890", FirstName = "Павел", LastName = "Дмитриев", Gender = Gender.Male, BirthDate = new(1996, 9, 5), Phone = "+7 (911) 100-00-05", MembershipStartDate = today.AddDays(-74), MembershipEndDate = today.AddDays(-14) },
            new() { Id = G(105), PassportNumber = "4506 678901", FirstName = "Ольга", LastName = "Егорова", Gender = Gender.Female, BirthDate = new(1983, 12, 1), Phone = "+7 (911) 100-00-06", MembershipStartDate = today.AddDays(-90), MembershipEndDate = today.AddDays(90) },
            new() { Id = G(106), PassportNumber = "4507 789012", FirstName = "Игорь", LastName = "Жуков", Gender = Gender.Male, BirthDate = new(1994, 5, 22), Phone = "+7 (911) 100-00-07", MembershipStartDate = today.AddDays(-30), MembershipEndDate = today.AddDays(30) },
            new() { Id = G(107), PassportNumber = "4508 890123", FirstName = "Анна", LastName = "Зотова", Gender = Gender.Female, BirthDate = new(1999, 8, 17), Phone = "+7 (911) 100-00-08", MembershipStartDate = today.AddDays(-60), MembershipEndDate = today.AddDays(15) },
            new() { Id = G(108), PassportNumber = "4509 901234", FirstName = "Сергей", LastName = "Иванов", Gender = Gender.Male, BirthDate = new(1975, 1, 9), Phone = "+7 (911) 100-00-09", MembershipStartDate = today.AddDays(-10), MembershipEndDate = today.AddDays(80) },
            new() { Id = G(109), PassportNumber = "4510 012345", FirstName = "Мария", LastName = "Козлова", Gender = Gender.Female, BirthDate = new(2001, 3, 28), Phone = "+7 (911) 100-00-10", MembershipStartDate = today.AddDays(-1), MembershipEndDate = today.AddDays(364) },
            new() { Id = G(110), PassportNumber = "4511 111111", FirstName = "Артём", LastName = "Лебедев", Gender = Gender.Male, BirthDate = new(2003, 7, 7), Phone = "+7 (911) 100-00-11", MembershipStartDate = today.AddDays(7), MembershipEndDate = today.AddDays(97) },
            new() { Id = G(111), PassportNumber = "4512 222222", FirstName = "Татьяна", LastName = "Морозова", Gender = Gender.Female, BirthDate = new(1988, 10, 10), Phone = "+7 (911) 100-00-12", MembershipStartDate = today.AddDays(14), MembershipEndDate = today.AddDays(104) },
        ];

        IReadOnlyList<Trainer> trainers =
        [
            new() { Id = G(200), PassportNumber = "4601 123456", FirstName = "Виктор", LastName = "Соколов", Gender = Gender.Male, BirthDate = new(1990, 6, 15), SpecializationId = G(2), ExperienceYears = 3 },
            new() { Id = G(201), PassportNumber = "4602 234567", FirstName = "Наталья", LastName = "Кузнецова", Gender = Gender.Female, BirthDate = new(1988, 3, 24), SpecializationId = G(1), ExperienceYears = 5 },
            new() { Id = G(202), PassportNumber = "4603 345678", FirstName = "Максим", LastName = "Попов", Gender = Gender.Male, BirthDate = new(1998, 11, 2), SpecializationId = G(4), ExperienceYears = 2 },
            new() { Id = G(203), PassportNumber = "4604 456789", FirstName = "Дарья", LastName = "Лебедева", Gender = Gender.Female, BirthDate = new(1985, 7, 19), SpecializationId = G(0), ExperienceYears = 8 },
            new() { Id = G(204), PassportNumber = "4605 567890", FirstName = "Артур", LastName = "Новиков", Gender = Gender.Male, BirthDate = new(1980, 1, 30), SpecializationId = G(5), ExperienceYears = 12 },
            new() { Id = G(205), PassportNumber = "4606 678901", FirstName = "Ксения", LastName = "Фёдорова", Gender = Gender.Female, BirthDate = new(1995, 9, 8), SpecializationId = G(6), ExperienceYears = 4 },
            new() { Id = G(206), PassportNumber = "4607 789012", FirstName = "Георгий", LastName = "Морозов", Gender = Gender.Male, BirthDate = new(1982, 12, 11), SpecializationId = G(3), ExperienceYears = 10 },
            new() { Id = G(207), PassportNumber = "4608 890123", FirstName = "Ирина", LastName = "Волкова", Gender = Gender.Female, BirthDate = new(1987, 5, 27), SpecializationId = G(7), ExperienceYears = 7 },
            new() { Id = G(208), PassportNumber = "4609 901234", FirstName = "Роман", LastName = "Зайцев", Gender = Gender.Male, BirthDate = new(2000, 2, 14), SpecializationId = G(8), ExperienceYears = 1 },
            new() { Id = G(209), PassportNumber = "4610 012345", FirstName = "Светлана", LastName = "Павлова", Gender = Gender.Female, BirthDate = new(1991, 8, 3), SpecializationId = G(0), ExperienceYears = 6 },
            new() { Id = G(210), PassportNumber = "4611 111111", FirstName = "Кирилл", LastName = "Семёнов", Gender = Gender.Male, BirthDate = new(1979, 4, 21), SpecializationId = G(2), ExperienceYears = 15 },
            new() { Id = G(211), PassportNumber = "4612 222222", FirstName = "Вероника", LastName = "Голубева", Gender = Gender.Female, BirthDate = new(1993, 10, 16), SpecializationId = G(9), ExperienceYears = 9 },
        ];

        IReadOnlyList<Booking> bookings =
        [
            new() { Id = G(300), ClientId = clients[5].Id, TrainerId = trainers[7].Id, ScheduledAt = At(currentMonth, 5, 10), HallName = "Силовой зал", IsTrial = false },
            new() { Id = G(301), ClientId = clients[6].Id, TrainerId = trainers[4].Id, ScheduledAt = At(currentMonth, 12, 12), HallName = "Силовой зал", IsTrial = false },
            new() { Id = G(302), ClientId = clients[7].Id, TrainerId = trainers[10].Id, ScheduledAt = At(currentMonth, 20, 18), HallName = "Силовой зал", IsTrial = true },
            new() { Id = G(303), ClientId = clients[5].Id, TrainerId = trainers[7].Id, ScheduledAt = At(previousMonth, 5, 10), HallName = "Силовой зал", IsTrial = false },
            new() { Id = G(304), ClientId = clients[5].Id, TrainerId = trainers[0].Id, ScheduledAt = At(nextMonth, 5, 10), HallName = "Силовой зал", IsTrial = false },
            new() { Id = G(305), ClientId = clients[9].Id, TrainerId = trainers[7].Id, ScheduledAt = moment.AddMinutes(-30), HallName = "Зал групповых занятий", IsTrial = false },
            new() { Id = G(306), ClientId = clients[0].Id, TrainerId = trainers[6].Id, ScheduledAt = moment.AddMinutes(-70), HallName = "Зал единоборств", IsTrial = false },
            new() { Id = G(307), ClientId = clients[6].Id, TrainerId = trainers[7].Id, ScheduledAt = At(currentMonth, 8, 10), HallName = "Бассейн", IsTrial = false },
            new() { Id = G(308), ClientId = clients[9].Id, TrainerId = trainers[4].Id, ScheduledAt = At(currentMonth, 14, 19), HallName = "Зал единоборств", IsTrial = false },
            new() { Id = G(309), ClientId = clients[8].Id, TrainerId = trainers[10].Id, ScheduledAt = At(previousMonth, 15, 8), HallName = "Бассейн", IsTrial = false },
            new() { Id = G(310), ClientId = clients[5].Id, TrainerId = trainers[1].Id, ScheduledAt = At(currentMonth, 22, 9), HallName = "Зал групповых занятий", IsTrial = true },
            new() { Id = G(311), ClientId = clients[6].Id, TrainerId = trainers[7].Id, ScheduledAt = At(previousMonth, 18, 17), HallName = "Силовой зал", IsTrial = false },
            new() { Id = G(312), ClientId = clients[7].Id, TrainerId = trainers[4].Id, ScheduledAt = At(nextMonth, 10, 11), HallName = "Кардио-зал", IsTrial = true },
            new() { Id = G(313), ClientId = clients[11].Id, TrainerId = trainers[10].Id, ScheduledAt = At(nextMonth, 3, 7), HallName = "Бассейн", IsTrial = true },
            new() { Id = G(314), ClientId = clients[1].Id, TrainerId = trainers[1].Id, ScheduledAt = At(previousMonth, 22, 20), HallName = "Зал единоборств", IsTrial = false },
            new() { Id = G(315), ClientId = clients[2].Id, TrainerId = trainers[6].Id, ScheduledAt = At(previousMonth, 28, 18), HallName = "Зал групповых занятий", IsTrial = false },
            new() { Id = G(316), ClientId = clients[8].Id, TrainerId = trainers[7].Id, ScheduledAt = At(currentMonth, 25, 8), HallName = "Бассейн", IsTrial = false },
            new() { Id = G(317), ClientId = clients[0].Id, TrainerId = trainers[4].Id, ScheduledAt = At(previousMonth, 10, 19), HallName = "Кардио-зал", IsTrial = false },
            new() { Id = G(318), ClientId = clients[5].Id, TrainerId = trainers[10].Id, ScheduledAt = At(currentMonth, 9, 21), HallName = "Зал единоборств", IsTrial = false },
            new() { Id = G(319), ClientId = clients[9].Id, TrainerId = trainers[1].Id, ScheduledAt = At(currentMonth, 16, 7), HallName = "Кардио-зал", IsTrial = false },
            new() { Id = G(320), ClientId = clients[7].Id, TrainerId = trainers[8].Id, ScheduledAt = At(previousMonth, 3, 18), HallName = "Бассейн", IsTrial = true },
            new() { Id = G(321), ClientId = clients[6].Id, TrainerId = trainers[4].Id, ScheduledAt = At(nextMonth, 14, 18), HallName = "Зал единоборств", IsTrial = false },
            new() { Id = G(322), ClientId = clients[1].Id, TrainerId = trainers[2].Id, ScheduledAt = At(previousMonth, 8, 8), HallName = "Кардио-зал", IsTrial = true },
            new() { Id = G(323), ClientId = clients[8].Id, TrainerId = trainers[9].Id, ScheduledAt = At(currentMonth, 17, 20), HallName = "Бассейн", IsTrial = false },
            new() { Id = G(324), ClientId = clients[9].Id, TrainerId = trainers[3].Id, ScheduledAt = At(nextMonth, 21, 10), HallName = "Зал групповых занятий", IsTrial = true },
            new() { Id = G(325), ClientId = clients[5].Id, TrainerId = trainers[11].Id, ScheduledAt = At(currentMonth, 27, 8), HallName = "Кардио-зал", IsTrial = false },
        ];

        return new ClubData
        {
            Specializations = specializations,
            Clients = clients,
            Trainers = trainers,
            Bookings = bookings,
        };
    }
}