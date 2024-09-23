namespace Pavas.Patterns.UnitOfWork.Contracts;

/// <summary>
/// Represents an entity or service that is associated with a specific tenant in a multi-tenant system.
/// </summary>
public interface ITenancy
{
    /// <summary>
    /// Gets or sets the tenant ID associated with the entity or service.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the unique identifier for the tenant.
    /// </value>
    public string TenantId { get; set; }
}