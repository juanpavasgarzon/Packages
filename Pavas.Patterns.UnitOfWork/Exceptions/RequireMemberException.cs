namespace Pavas.Patterns.UnitOfWork.Exceptions;

/// <summary>
/// Represents errors that occur when a required member (property, field, or method) is missing or not set.
/// This exception is typically thrown when a critical member required for an operation is not provided or initialized.
/// </summary>
public class RequireMemberException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RequireMemberException"/> class.
    /// </summary>
    public RequireMemberException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RequireMemberException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public RequireMemberException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RequireMemberException"/> class with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception. If the <paramref name="innerException"/> parameter
    /// is not null, the current exception is raised in a catch block that handles the inner exception.
    /// </param>
    public RequireMemberException(string message, Exception innerException) : base(message, innerException)
    {
    }
}