namespace Pavas.Patterns.UnitOfWork.Contracts;

public interface IEntity
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

    /// <summary>
    /// Gets or sets the identifier for the database tenant.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the tenant's ID.
    /// </value>
    public string TenantId { get; set; }
}