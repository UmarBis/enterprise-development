namespace FitnessClub.Application.Exceptions;

/// <summary>Исключение, возникающее при конфликте с текущим состоянием данных.</summary>
public sealed class ConflictException(string message) : Exception(message)
{
}
