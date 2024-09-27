namespace Pavas.Patterns.UnitOfWork.Exceptions;

/// <summary>
/// Represents errors that occur when a requested entity or resource is not found.
/// This exception is typically thrown when an attempt to retrieve an entity or resource by its identifier
/// fails because the entity or resource does not exist.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with no error message.
    /// </summary>
    public NotFoundException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public NotFoundException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception. If the <paramref name="innerException"/> parameter
    /// is not null, the current exception is raised in a catch block that handles the inner exception.
    /// </param>
    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}