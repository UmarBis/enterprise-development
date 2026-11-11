using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Services;

public interface ITrainerService
{
    Task<IReadOnlyList<TrainerDto>> GetAllAsync(CancellationToken ct = default);
    Task<TrainerDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<TrainerDto> CreateAsync(UpsertTrainerDto dto, CancellationToken ct = default);
    Task<TrainerDto?> UpdateAsync(Guid id, UpsertTrainerDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}