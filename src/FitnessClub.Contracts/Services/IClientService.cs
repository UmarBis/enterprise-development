using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Services;

public interface IClientService
{
    Task<IReadOnlyList<ClientDto>> GetAllAsync(CancellationToken ct = default);
    Task<ClientDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ClientDto> CreateAsync(UpsertClientDto dto, CancellationToken ct = default);
    Task<ClientDto?> UpdateAsync(Guid id, UpsertClientDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}