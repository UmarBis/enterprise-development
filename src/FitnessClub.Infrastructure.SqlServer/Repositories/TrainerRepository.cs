using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.SqlServer.Repositories;

public sealed class TrainerRepository(FitnessClubDbContext db) : ITrainerRepository
{
    public async Task<IReadOnlyList<Trainer>> GetAllAsync(CancellationToken ct = default)
        => await db.Trainers.AsNoTracking().ToListAsync(ct);

    public Task<Trainer?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => db.Trainers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<Trainer> AddAsync(Trainer trainer, CancellationToken ct = default)
    {
        db.Trainers.Add(trainer);
        await db.SaveChangesAsync(ct);
        return trainer;
    }

    public async Task<bool> UpdateAsync(Guid id, Trainer trainer, CancellationToken ct = default)
    {
        var current = await db.Trainers.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (current is null) return false;
        db.Entry(current).CurrentValues.SetValues(trainer);
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var current = await db.Trainers.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (current is null) return false;
        db.Trainers.Remove(current);
        await db.SaveChangesAsync(ct);
        return true;
    }
}
