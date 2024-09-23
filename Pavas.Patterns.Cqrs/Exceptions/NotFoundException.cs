namespace Pavas.Patterns.Cqrs.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a requested resource or entity is not found.
/// </summary>
internal class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    internal NotFoundException(string message) : base(message)
    {
    }
}