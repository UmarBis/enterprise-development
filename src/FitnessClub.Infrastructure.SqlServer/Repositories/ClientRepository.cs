using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.SqlServer.Repositories;

public sealed class ClientRepository(FitnessClubDbContext db) : IClientRepository
{
    public async Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken ct = default)
        => await db.Clients.AsNoTracking().ToListAsync(ct);

    public Task<Client?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => db.Clients.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<Client> AddAsync(Client client, CancellationToken ct = default)
    {
        db.Clients.Add(client);
        await db.SaveChangesAsync(ct);
        return client;
    }

    public async Task<bool> UpdateAsync(Guid id, Client client, CancellationToken ct = default)
    {
        var current = await db.Clients.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (current is null) return false;
        db.Entry(current).CurrentValues.SetValues(client);
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var current = await db.Clients.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (current is null) return false;
        db.Clients.Remove(current);
        await db.SaveChangesAsync(ct);
        return true;
    }
}
