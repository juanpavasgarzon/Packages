namespace Pavas.Patterns.UnitOfWork.Contracts;

public interface ISoftDelete
{
    /// <summary>
    /// Gets or sets the date and time when the entity was soft deleted.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> representing the deletion timestamp.
    /// </value>
    public DateTime DeletedAt { get; set; }
}