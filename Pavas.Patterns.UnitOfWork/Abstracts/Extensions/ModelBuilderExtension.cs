using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Pavas.Patterns.UnitOfWork.Abstracts.Extensions;

internal static class ModelBuilderExtension
{
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