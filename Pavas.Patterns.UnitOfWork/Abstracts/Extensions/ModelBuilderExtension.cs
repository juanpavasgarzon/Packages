using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork.Abstracts.Extensions;

public static class ModelBuilderExtension
{
    public static void HasQueryFilter<TEntity>(this ModelBuilder builder, Expression<Func<IEntity, bool>> filter)
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