using FitnessClub.Application.Exceptions;
using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;

namespace FitnessClub.Application.Services;

/// <summary>Управляет тренерами фитнес-клуба.</summary>
public class TrainerService(
    ITrainerRepository trainers,
    ISpecializationRepository specializations,
    IBookingRepository bookings) : ITrainerService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<TrainerDto>> GetAllAsync(CancellationToken ct = default)
    {
        var names = (await specializations.GetAllAsync(ct))
            .ToDictionary(specialization => specialization.Id, specialization => specialization.Name);

        return [.. (await trainers.GetAllAsync(ct))
            .Select(trainer => trainer.ToDto(
                names.GetValueOrDefault(trainer.SpecializationId, string.Empty)))];
    }

    /// <inheritdoc/>
    public async Task<TrainerDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var trainer = await trainers.GetByIdAsync(id, ct);
        if (trainer is null) return null;

        var specialization = await specializations.GetByIdAsync(trainer.SpecializationId, ct);
        return trainer.ToDto(specialization?.Name ?? string.Empty);
    }

    /// <inheritdoc/>
    public async Task<TrainerDto> CreateAsync(UpsertTrainerDto dto, CancellationToken ct = default)
    {
        var specialization = await GetSpecializationAsync(dto.SpecializationId, ct);
        var entity = CreateEntity(dto);
        await trainers.AddAsync(entity, ct);
        return entity.ToDto(specialization.Name);
    }

    /// <inheritdoc/>
    public async Task<TrainerDto?> UpdateAsync(Guid id, UpsertTrainerDto dto, CancellationToken ct = default)
    {
        if (await trainers.GetByIdAsync(id, ct) is null) return null;

        var specialization = await GetSpecializationAsync(dto.SpecializationId, ct);
        var updated = CreateEntity(dto, id);
        await trainers.UpdateAsync(id, updated, ct);
        return updated.ToDto(specialization.Name);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        if (await trainers.GetByIdAsync(id, ct) is null) return false;

        if ((await bookings.GetAllAsync(ct)).Any(booking => booking.TrainerId == id))
            throw new ConflictException($"Нельзя удалить тренера {id}: к нему есть записи на занятия.");

        return await trainers.DeleteAsync(id, ct);
    }

    private async Task<FitnessClub.Domain.Entities.Specialization> GetSpecializationAsync(
        Guid? specializationId, CancellationToken ct)
    {
        if (specializationId is null || specializationId == Guid.Empty)
            throw new ArgumentException("Необходимо указать корректную специализацию тренера.");

        return await specializations.GetByIdAsync(specializationId.Value, ct)
            ?? throw new EntityNotFoundException($"Специализация {specializationId} не найдена.");
    }

    private static Trainer CreateEntity(UpsertTrainerDto dto, Guid? id = null)
        => new()
        {
            Id = id ?? Guid.NewGuid(),
            PassportNumber = dto.PassportNumber,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = dto.Gender!.Value,
            BirthDate = dto.BirthDate!.Value,
            SpecializationId = dto.SpecializationId!.Value,
            ExperienceYears = dto.ExperienceYears
        };
}
