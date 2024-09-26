using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Pavas.Patterns.UnitOfWork.Abstracts.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="ModelBuilder"/> class to support custom query filters for multiple entity types.
/// </summary>
internal static class ModelBuilderExtension
{
    /// <summary>
    /// Applies a global query filter for all entities of type <typeparamref name="TEntity"/> or any of its derived types.
    /// This filter ensures that the specified condition is automatically applied to all queries involving these entities.
    /// </summary>
    /// <typeparam name="TEntity">The base entity type to which the filter will be applied. All derived entity types will also inherit this filter.</typeparam>
    /// <param name="builder">The <see cref="ModelBuilder"/> instance used to configure the entity model.</param>
    /// <param name="filter">An expression that represents the condition to be applied as a query filter.</param>
    public static void HasQueryFilter<TEntity>(this ModelBuilder builder, Expression<Func<TEntity, bool>> filter)
    {
        var types = builder.Model.GetEntityTypes()
            .Select(entityType => entityType.ClrType)
            .Where(type => typeof(TEntity).IsAssignableFrom(type));

        foreach (var entityType in types)
        {
            var replacement = Expression.Parameter(entityType);
            var singleParams = filter.Parameters.Single();
            var body = ReplacingExpressionVisitor.Replace(singleParams, replacement, filter.Body);
            var lambda = Expression.Lambda(body, replacement);
            builder.Entity(entityType).HasQueryFilter(lambda);
        }
    }
}