namespace Pavas.Patterns.UnitOfWork.Contracts.Entities;

/// <summary>
/// Represents a base entity with common properties for auditing and tenant management.
/// </summary>
public interface ITimestamps
{
    /// <summary>
    /// Gets or sets the date and time when the entity was created.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> representing the creation timestamp.
    /// </value>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was last modified.
    /// </summary>
    /// <value>
    /// A nullable <see cref="DateTime"/> representing the last modification timestamp, or null if the entity hasn't been modified.
    /// </value>
    public DateTime? UpdatedAt { get; set; }
}