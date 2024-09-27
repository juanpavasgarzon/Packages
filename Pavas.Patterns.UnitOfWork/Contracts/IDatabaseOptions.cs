namespace Pavas.Patterns.UnitOfWork.Contracts;

public interface IDatabaseOptions
{
    /// <summary>
    /// Gets or sets the connection string used to connect to the database.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the connection string for the database.
    /// </value>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the default tenant ID to be used for database records.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the default tenant ID.
    /// </value>
    public string TenantId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating a correlation logic is enabled for entities implementing <see cref="ICorrelated"/>.
    /// Gets or sets the correlation ID, which is used to track related operations across different services and repositories.
    /// </summary>
    public string CorrelationId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether create database logic />.
    /// </summary>
    /// <value>
    /// A <see cref="bool"/> indicating whether ensure create database is enabled.
    /// </value>
    public bool EnsureCreated { get; set; }
}