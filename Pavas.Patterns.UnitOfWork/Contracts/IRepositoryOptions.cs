namespace Pavas.Patterns.UnitOfWork.Contracts;

/// <summary>
/// Defines the options that can be configured for a repository, including tenant and correlation identifiers.
/// </summary>
public interface IRepositoryOptions
{
    /// <summary>
    /// Gets or sets the tenant ID, which is used to scope repository operations to a specific tenant in a multi-tenant environment.
    /// </summary>
    public string TenantId { get; set; }

    /// <summary>
    /// Gets or sets the correlation ID, which is used to track related operations across different services and repositories.
    /// </summary>
    public string CorrelationId { get; set; }
}