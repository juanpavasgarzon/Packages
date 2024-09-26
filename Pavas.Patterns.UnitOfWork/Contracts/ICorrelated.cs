namespace Pavas.Patterns.UnitOfWork.Contracts;

/// <summary>
/// Represents an entity that is associated with a correlation ID, 
/// which can be used to track related events or actions in a distributed system.
/// </summary>
public interface ICorrelated
{
    /// <summary>
    /// Gets or sets the Correlation ID, which uniquely identifies related actions or events
    /// across multiple services or components within a system.
    /// </summary>
    string CorrelationId { get; set; }
}