using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.SqlServer.Repositories;

public sealed class SpecializationRepository(FitnessClubDbContext db) : ISpecializationRepository
{
    public async Task<IReadOnlyList<Specialization>> GetAllAsync(CancellationToken ct = default)
        => await db.Specializations.AsNoTracking().ToListAsync(ct);

    public Task<Specialization?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => db.Specializations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<Specialization> AddAsync(Specialization specialization, CancellationToken ct = default)
    {
        db.Specializations.Add(specialization);
        await db.SaveChangesAsync(ct);
        return specialization;
    }

    public async Task<bool> UpdateAsync(Guid id, Specialization specialization, CancellationToken ct = default)
    {
        var current = await db.Specializations.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (current is null) return false;
        db.Entry(current).CurrentValues.SetValues(specialization);
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var current = await db.Specializations.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (current is null) return false;
        db.Specializations.Remove(current);
        await db.SaveChangesAsync(ct);
        return true;
    }
}
