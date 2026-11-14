namespace FitnessClub.Application.Exceptions;

/// <summary>Исключение, возникающее, когда связанная или запрошенная сущность не найдена.</summary>
public sealed class EntityNotFoundException(string message) : Exception(message)
{
}
