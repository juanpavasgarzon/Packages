using System.Runtime.CompilerServices;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork.Options;

internal class DatabaseOptions : IDatabaseOptions
{
    /// <summary>
    /// Gets or sets the connection string used to connect to the database.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the connection string for the database.
    /// </value>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the default tenant ID to be used for database records.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the default tenant ID.
    /// </value>
    public string TenantId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether soft delete logic is enabled for entities implementing <see cref="ISoftDelete"/>.
    /// </summary>
    /// <value>
    /// A <see cref="bool"/> indicating whether soft delete is enabled.
    /// </value>
    public bool SoftDelete { get; set; } = false;

    /// <summary>
    /// Gets or sets a value indicating whether create database logic />.
    /// </summary>
    /// <value>
    /// A <see cref="bool"/> indicating whether ensure create database is enabled.
    /// </value>
    public bool EnsureCreated { get; set; } = false;

    /// <summary>
    /// Returns a string representation of the current database options, including connection string, soft delete, and ensure created settings.
    /// This method uses an interpolated string handler for efficient string concatenation.
    /// </summary>
    /// <returns>
    /// A string that represents the current settings of the database options, 
    /// including "ConnectionString", "SoftDelete", and "EnsureCreated" properties.
    /// </returns>
    public override string ToString()
    {
        var interpolatedStringHandler = new DefaultInterpolatedStringHandler();
        interpolatedStringHandler.AppendLiteral("ConnectionString:");
        interpolatedStringHandler.AppendFormatted(ConnectionString);
        interpolatedStringHandler.AppendLiteral("SoftDelete:");
        interpolatedStringHandler.AppendFormatted(SoftDelete);
        interpolatedStringHandler.AppendLiteral("EnsureCreated:");
        interpolatedStringHandler.AppendFormatted(EnsureCreated);
        return interpolatedStringHandler.ToStringAndClear();
    }
}