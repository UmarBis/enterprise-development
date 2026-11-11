using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

public interface ITrainerRepository
{
    Task<IReadOnlyList<Trainer>> GetAllAsync(CancellationToken ct = default);
    Task<Trainer?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Trainer> AddAsync(Trainer trainer, CancellationToken ct = default);
    Task<bool> UpdateAsync(Guid id, Trainer trainer, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}