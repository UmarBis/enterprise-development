using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Application.Services;

public class SpecializationService : ISpecializationService
{
    private readonly ISpecializationRepository _repo;
    public SpecializationService(ISpecializationRepository repo) => _repo = repo;

    public async Task<IReadOnlyList<SpecializationDto>> GetAllAsync(CancellationToken ct = default)
        => (await _repo.GetAllAsync(ct)).Select(s => s.ToDto()).ToList();

    public async Task<SpecializationDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => (await _repo.GetByIdAsync(id, ct))?.ToDto();

    public async Task<SpecializationDto> CreateAsync(UpsertSpecializationDto dto, CancellationToken ct = default)
        => (await _repo.AddAsync(new Specialization { Name = dto.Name }, ct)).ToDto();

    public async Task<SpecializationDto?> UpdateAsync(Guid id, UpsertSpecializationDto dto, CancellationToken ct = default)
    {
        var exists = await _repo.GetByIdAsync(id, ct);
        if (exists is null) return null;
        var updated = new Specialization { Id = id, Name = dto.Name };
        await _repo.UpdateAsync(id, updated, ct);
        return updated.ToDto();
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => _repo.DeleteAsync(id, ct);
}