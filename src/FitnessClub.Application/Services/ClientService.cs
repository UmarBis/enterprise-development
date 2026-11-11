using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clients;
    private readonly Func<DateTime> _now;

    public ClientService(IClientRepository clients, Func<DateTime> now)
        => (_clients, _now) = (clients, now);

    public async Task<IReadOnlyList<ClientDto>> GetAllAsync(CancellationToken ct = default)
        => (await _clients.GetAllAsync(ct)).Select(c => c.ToDto()).ToList();

    public async Task<ClientDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => (await _clients.GetByIdAsync(id, ct))?.ToDto();

    public async Task<ClientDto> CreateAsync(UpsertClientDto dto, CancellationToken ct = default)
    {
        var entity = new Client
        {
            PassportNumber = dto.PassportNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = dto.Gender!.Value,
            BirthDate = dto.BirthDate!.Value,
            Phone = dto.Phone,
            MembershipStartDate = dto.SubscriptionStart!.Value,
            MembershipEndDate = dto.SubscriptionEnd!.Value
        };
        return (await _clients.AddAsync(entity, ct)).ToDto();
    }

    public async Task<ClientDto?> UpdateAsync(Guid id, UpsertClientDto dto, CancellationToken ct = default)
    {
        var exists = await _clients.GetByIdAsync(id, ct);
        if (exists is null) return null;
        var updated = new Client
        {
            Id = id,
            PassportNumber = dto.PassportNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = dto.Gender!.Value,
            BirthDate = dto.BirthDate!.Value,
            Phone = dto.Phone,
            MembershipStartDate = dto.SubscriptionStart!.Value,
            MembershipEndDate = dto.SubscriptionEnd!.Value
        };
        await _clients.UpdateAsync(id, updated, ct);
        return updated.ToDto();
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => _clients.DeleteAsync(id, ct);

    public async Task<IReadOnlyList<ClientDto>> GetClientsWithExpiredSubscriptionAsync(CancellationToken ct = default)
    {
        var today = DateOnly.FromDateTime(_now());
        return (await _clients.GetAllAsync(ct))
            .Where(c => c.IsMembershipExpired(today))
            .OrderBy(c => c.FullName)
            .Select(c => c.ToDto())
            .ToList();
    }
}