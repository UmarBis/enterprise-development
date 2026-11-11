using FitnessClub.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Infrastructure.InMemory;

public static class DependencyInjection
{
    public static IServiceCollection AddInMemoryInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IClientRepository, InMemoryClientRepository>();
        services.AddSingleton<ITrainerRepository, InMemoryTrainerRepository>();
        services.AddSingleton<ISpecializationRepository, InMemorySpecializationRepository>();
        services.AddSingleton<IBookingRepository, InMemoryBookingRepository>();
        return services;
    }
}