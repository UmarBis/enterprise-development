using FitnessClub.Domain.Repositories;
using FitnessClub.Domain.Seeds;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Infrastructure.InMemory;

/// <summary>Регистрация in-memory реализаций репозиториев и начальных данных.</summary>
public static class DependencyInjection
{
    /// <summary>Добавляет хранилища в памяти, заполненные тестовыми данными.</summary>
    public static IServiceCollection AddInMemoryInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton(sp => ClubDataSeeder.Create());

        services.AddSingleton<IClientRepository>(sp =>
            new InMemoryClientRepository(sp.GetRequiredService<ClubData>().Clients));
        services.AddSingleton<ITrainerRepository>(sp =>
            new InMemoryTrainerRepository(sp.GetRequiredService<ClubData>().Trainers));
        services.AddSingleton<ISpecializationRepository>(sp =>
            new InMemorySpecializationRepository(sp.GetRequiredService<ClubData>().Specializations));
        services.AddSingleton<IBookingRepository>(sp =>
            new InMemoryBookingRepository(sp.GetRequiredService<ClubData>().Bookings));

        return services;
    }
}
