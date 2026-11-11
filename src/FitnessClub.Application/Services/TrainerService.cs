using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Application.Services;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _trainers;
    private readonly ISpecializationRepository _specs;
    private readonly IBookingRepository _bookings;

    public TrainerService(ITrainerRepository trainers, ISpecializationRepository specs, IBookingRepository bookings)
        => (_trainers, _specs, _bookings) = (trainers, specs, bookings);

    public async Task<IReadOnlyList<TrainerDto>> GetAllAsync(CancellationToken ct = default)
    {
        var specs = (await _specs.GetAllAsync(ct)).ToDictionary(s => s.Id, s => s.Name);
        return (await _trainers.GetAllAsync(ct))
            .Select(t => t.ToDto(specs.GetValueOrDefault(t.SpecializationId, string.Empty)))
            .ToList();
    }

    public async Task<TrainerDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var t = await _trainers.GetByIdAsync(id, ct);
        if (t is null) return null;
        var spec = await _specs.GetByIdAsync(t.SpecializationId, ct);
        return t.ToDto(spec?.Name ?? string.Empty);
    }

    public async Task<TrainerDto> CreateAsync(UpsertTrainerDto dto, CancellationToken ct = default)
    {
        var spec = await _specs.GetByIdAsync(dto.SpecializationId!.Value, ct)
            ?? throw new InvalidOperationException($"Специализация {dto.SpecializationId} не найдена.");
        var entity = new Trainer
        {
            PassportNumber = dto.PassportNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = dto.Gender!.Value,
            BirthDate = dto.BirthDate!.Value,
            SpecializationId = dto.SpecializationId!.Value,
            ExperienceYears = dto.ExperienceYears
        };
        await _trainers.AddAsync(entity, ct);
        return entity.ToDto(spec.Name);
    }

    public async Task<TrainerDto?> UpdateAsync(Guid id, UpsertTrainerDto dto, CancellationToken ct = default)
    {
        var exists = await _trainers.GetByIdAsync(id, ct);
        if (exists is null) return null;
        var spec = await _specs.GetByIdAsync(dto.SpecializationId!.Value, ct)
            ?? throw new InvalidOperationException($"Специализация {dto.SpecializationId} не найдена.");
        var updated = new Trainer
        {
            Id = id,
            PassportNumber = dto.PassportNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = dto.Gender!.Value,
            BirthDate = dto.BirthDate!.Value,
            SpecializationId = dto.SpecializationId!.Value,
            ExperienceYears = dto.ExperienceYears
        };
        await _trainers.UpdateAsync(id, updated, ct);
        return updated.ToDto(spec.Name);
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        => _trainers.DeleteAsync(id, ct);

    public async Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(int minYears = 5, CancellationToken ct = default)
    {
        var specs = (await _specs.GetAllAsync(ct)).ToDictionary(s => s.Id, s => s.Name);
        return (await _trainers.GetAllAsync(ct))
            .Where(t => t.ExperienceYears >= minYears)
            .Select(t => t.ToDto(specs.GetValueOrDefault(t.SpecializationId, string.Empty)))
            .ToList();
    }

    public async Task<IReadOnlyList<TrainerDto>> GetPopularTrainersAsync(int count = 5, CancellationToken ct = default)
    {
        var specs = (await _specs.GetAllAsync(ct)).ToDictionary(s => s.Id, s => s.Name);
        var all = await _trainers.GetAllAsync(ct);
        var counts = new Dictionary<Guid, int>();
        foreach (var t in all) counts[t.Id] = await _bookings.CountByTrainerAsync(t.Id, ct);
        return all
            .OrderByDescending(t => counts[t.Id])
            .Take(count)
            .Select(t => t.ToDto(specs.GetValueOrDefault(t.SpecializationId, string.Empty)))
            .ToList();
    }
}