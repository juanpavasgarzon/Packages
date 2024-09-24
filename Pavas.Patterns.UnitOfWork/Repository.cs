using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pavas.Patterns.UnitOfWork.Contracts;

namespace Pavas.Patterns.UnitOfWork;

internal class Repository<TEntity>(
    DbContext context,
    CancellationToken token = new()
) : IRepository<TEntity> where TEntity : class
{
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await context.Set<TEntity>().FindAsync([id], token);
    }

    public Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> filter)
    {
        return context.Set<TEntity>().Where(filter).FirstOrDefaultAsync(token);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().ToListAsync(token);
    }

    public async Task<IQueryable<TEntity>> GetQueryAsync()
    {
        return await Task.Run(() => context.Set<TEntity>().AsQueryable(), token);
    }

    public async Task AddAsync(TEntity entry)
    {
        await context.Set<TEntity>().AddAsync(entry, token);
    }

    public async Task AddManyAsync(IEnumerable<TEntity> entries)
    {
        await context.Set<TEntity>().AddRangeAsync(entries, token);
    }

    public async Task UpdateAsync(TEntity entry)
    {
        await Task.Run(() => context.Set<TEntity>().Update(entry), token);
    }

    public async Task RemoveAsync(TEntity entry)
    {
        await Task.Run(() => context.Set<TEntity>().Remove(entry), token);
    }

    public async Task RemoveManyAsync(IEnumerable<TEntity> entries)
    {
        await Task.Run(() => context.Set<TEntity>().RemoveRange(entries), token);
    }
}