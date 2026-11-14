using FitnessClub.Application.Exceptions;
using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Application.Services;

/// <summary>Управляет справочником специализаций тренеров.</summary>
public class SpecializationService(
    ISpecializationRepository repository,
    ITrainerRepository trainers) : ISpecializationService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<SpecializationDto>> GetAllAsync(CancellationToken ct = default)
        => [.. (await repository.GetAllAsync(ct)).Select(specialization => specialization.ToDto())];

    /// <inheritdoc/>
    public async Task<SpecializationDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => (await repository.GetByIdAsync(id, ct))?.ToDto();

    /// <inheritdoc/>
    public async Task<SpecializationDto> CreateAsync(
        UpsertSpecializationDto dto, CancellationToken ct = default)
        => (await repository.AddAsync(new Specialization { Name = dto.Name.Trim() }, ct)).ToDto();

    /// <inheritdoc/>
    public async Task<SpecializationDto?> UpdateAsync(
        Guid id, UpsertSpecializationDto dto, CancellationToken ct = default)
    {
        if (await repository.GetByIdAsync(id, ct) is null) return null;

        var updated = new Specialization { Id = id, Name = dto.Name.Trim() };
        await repository.UpdateAsync(id, updated, ct);
        return updated.ToDto();
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        if (await repository.GetByIdAsync(id, ct) is null) return false;

        if ((await trainers.GetAllAsync(ct)).Any(trainer => trainer.SpecializationId == id))
            throw new ConflictException($"Нельзя удалить специализацию {id}: она назначена тренерам.");

        return await repository.DeleteAsync(id, ct);
    }
}
