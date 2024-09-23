namespace Pavas.Patterns.UnitOfWork.Contracts;

/// <summary>
/// Represents an entity that supports soft delete functionality by maintaining a deletion timestamp.
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// Gets or sets a value indicating whether the entity has been soft-deleted.
    /// </summary>
    /// <value>
    /// A <see cref="bool"/> value, where <c>true</c> indicates the entity is soft-deleted, 
    /// and <c>false</c> means the entity is active.
    /// </value>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the entity was soft-deleted.
    /// This property is used to store the timestamp for when the entity was marked as deleted without being physically removed from the database.
    /// </summary>
    /// <value>
    /// A nullable <see cref="DateTime"/> representing the timestamp of the soft-deletion, 
    /// or <c>null</c> if the entity has not been soft-deleted.
    /// </value>
    public DateTime? DeletedAt { get; set; }
}