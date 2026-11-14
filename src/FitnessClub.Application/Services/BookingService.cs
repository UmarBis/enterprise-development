using FitnessClub.Application.Exceptions;
using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Application.Services;

/// <summary>Управляет записями клиентов на занятия.</summary>
public class BookingService(
    IBookingRepository bookings,
    IClientRepository clients,
    ITrainerRepository trainers) : IBookingService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<BookingDto>> GetAllAsync(CancellationToken ct = default)
        => [.. (await bookings.GetAllAsync(ct)).Select(booking => booking.ToDto())];

    /// <inheritdoc/>
    public async Task<BookingDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => (await bookings.GetByIdAsync(id, ct))?.ToDto();

    /// <inheritdoc/>
    public async Task<BookingDto> CreateAsync(UpsertBookingDto dto, CancellationToken ct = default)
    {
        await EnsureReferencesExistAsync(dto, ct);
        var entity = CreateEntity(dto);
        return (await bookings.AddAsync(entity, ct)).ToDto();
    }

    /// <inheritdoc/>
    public async Task<BookingDto?> UpdateAsync(Guid id, UpsertBookingDto dto, CancellationToken ct = default)
    {
        if (await bookings.GetByIdAsync(id, ct) is null) return null;

        await EnsureReferencesExistAsync(dto, ct);
        var updated = CreateEntity(dto, id);
        await bookings.UpdateAsync(id, updated, ct);
        return updated.ToDto();
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => bookings.DeleteAsync(id, ct);

    private async Task EnsureReferencesExistAsync(UpsertBookingDto dto, CancellationToken ct)
    {
        if (dto.ClientId is null || dto.ClientId == Guid.Empty)
            throw new ArgumentException("Необходимо указать корректный идентификатор клиента.");

        if (dto.TrainerId is null || dto.TrainerId == Guid.Empty)
            throw new ArgumentException("Необходимо указать корректный идентификатор тренера.");

        if (dto.SessionDateTime is null)
            throw new ArgumentException("Необходимо указать дату и время занятия.");

        if (string.IsNullOrWhiteSpace(dto.HallName))
            throw new ArgumentException("Необходимо указать название зала.");

        if (await clients.GetByIdAsync(dto.ClientId.Value, ct) is null)
            throw new EntityNotFoundException($"Клиент {dto.ClientId} не найден.");

        if (await trainers.GetByIdAsync(dto.TrainerId.Value, ct) is null)
            throw new EntityNotFoundException($"Тренер {dto.TrainerId} не найден.");
    }

    private static Booking CreateEntity(UpsertBookingDto dto, Guid? id = null)
        => new()
        {
            Id = id ?? Guid.NewGuid(),
            ClientId = dto.ClientId!.Value,
            TrainerId = dto.TrainerId!.Value,
            HallName = dto.HallName.Trim(),
            ScheduledAt = dto.SessionDateTime!.Value,
            IsTrial = dto.IsTrial
        };
}
