namespace Pavas.Patterns.Cqrs.Exceptions;

internal class NotFoundException : Exception
{
    internal NotFoundException(string message) : base(message)
    {
    }
}