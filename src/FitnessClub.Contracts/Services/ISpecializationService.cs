using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Services;

public interface ISpecializationService
{
    Task<IReadOnlyList<SpecializationDto>> GetAllAsync(CancellationToken ct = default);
    Task<SpecializationDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<SpecializationDto> CreateAsync(UpsertSpecializationDto dto, CancellationToken ct = default);
    Task<SpecializationDto?> UpdateAsync(Guid id, UpsertSpecializationDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}