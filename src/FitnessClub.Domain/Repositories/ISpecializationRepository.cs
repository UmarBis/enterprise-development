using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

public interface ISpecializationRepository
{
    Task<IReadOnlyList<Specialization>> GetAllAsync(CancellationToken ct = default);
    Task<Specialization?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Specialization> AddAsync(Specialization specialization, CancellationToken ct = default);
    Task<bool> UpdateAsync(Guid id, Specialization specialization, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}