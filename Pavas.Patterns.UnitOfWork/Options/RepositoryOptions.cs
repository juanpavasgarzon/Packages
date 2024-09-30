using Pavas.Patterns.UnitOfWork.Contracts.Options;

namespace Pavas.Patterns.UnitOfWork.Options;

/// <summary>
/// Defines the options that can be configured for a repository, including tenant and correlation identifiers.
/// </summary>
public class RepositoryOptions : IRepositoryOptions
{
    /// <summary>
    /// Gets or sets the tenant ID, which is used to scope repository operations to a specific tenant in a multi-tenant environment.
    /// </summary>
    public string TenantId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the correlation ID, which is used to track related operations across different services and repositories.
    /// </summary>
    public string CorrelationId { get; set; } = Guid.NewGuid().ToString();
}