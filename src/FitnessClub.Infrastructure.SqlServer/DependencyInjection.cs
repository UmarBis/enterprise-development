using FitnessClub.Domain.Repositories;
using FitnessClub.Domain.Seeds;
using FitnessClub.Infrastructure.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Infrastructure.SqlServer;

/// <summary>Регистрация SQL Server хранилища.</summary>
public static class DependencyInjection
{
    public static IServiceCollection AddSqlServerInfrastructure(
        this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<FitnessClubDbContext>(options => options.UseSqlServer(
            connectionString,
            sql => sql.MigrationsAssembly(typeof(FitnessClubDbContext).Assembly.FullName)
                .EnableRetryOnFailure()));

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ITrainerRepository, TrainerRepository>();
        services.AddScoped<ISpecializationRepository, SpecializationRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        return services;
    }

    /// <summary>Применяет миграции и добавляет исходный набор данных, если база пуста.</summary>
    public static async Task InitializeDatabaseAsync(this IServiceProvider services, CancellationToken ct = default)
    {
        await using var scope = services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<FitnessClubDbContext>();
        await db.Database.MigrateAsync(ct);

        if (await db.Specializations.AnyAsync(ct))
            return;

        var data = ClubDataSeeder.Create();
        await db.Specializations.AddRangeAsync(data.Specializations, ct);
        await db.Clients.AddRangeAsync(data.Clients, ct);
        await db.Trainers.AddRangeAsync(data.Trainers, ct);
        await db.Bookings.AddRangeAsync(data.Bookings, ct);
        await db.SaveChangesAsync(ct);
    }
}
