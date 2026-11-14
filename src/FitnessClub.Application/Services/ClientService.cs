using FitnessClub.Application.Exceptions;
using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Application.Services;

/// <summary>Управляет клиентами фитнес-клуба.</summary>
public class ClientService(
    IClientRepository clients,
    IBookingRepository bookings) : IClientService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<ClientDto>> GetAllAsync(CancellationToken ct = default)
        => [.. (await clients.GetAllAsync(ct)).Select(client => client.ToDto())];

    /// <inheritdoc/>
    public async Task<ClientDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => (await clients.GetByIdAsync(id, ct))?.ToDto();

    /// <inheritdoc/>
    public async Task<ClientDto> CreateAsync(UpsertClientDto dto, CancellationToken ct = default)
    {
        var entity = CreateEntity(dto);
        return (await clients.AddAsync(entity, ct)).ToDto();
    }

    /// <inheritdoc/>
    public async Task<ClientDto?> UpdateAsync(Guid id, UpsertClientDto dto, CancellationToken ct = default)
    {
        if (await clients.GetByIdAsync(id, ct) is null) return null;

        var updated = CreateEntity(dto, id);
        await clients.UpdateAsync(id, updated, ct);
        return updated.ToDto();
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        if (await clients.GetByIdAsync(id, ct) is null) return false;

        if ((await bookings.GetAllAsync(ct)).Any(booking => booking.ClientId == id))
            throw new ConflictException($"Нельзя удалить клиента {id}: у него есть записи на занятия.");

        return await clients.DeleteAsync(id, ct);
    }

    private static Client CreateEntity(UpsertClientDto dto, Guid? id = null)
        => new()
        {
            Id = id ?? Guid.NewGuid(),
            PassportNumber = dto.PassportNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = dto.Gender!.Value,
            BirthDate = dto.BirthDate!.Value,
            Phone = dto.Phone,
            MembershipStartDate = dto.SubscriptionStart!.Value,
            MembershipEndDate = dto.SubscriptionEnd!.Value
        };
}
