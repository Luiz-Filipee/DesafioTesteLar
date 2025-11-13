using Microsoft.EntityFrameworkCore;
using Lar.Domain.Interfaces;
using Lar.Infrastructure.Data;

namespace Lar.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _ctx;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext ctx)
    {
        _ctx = ctx;
        _dbSet = _ctx.Set<T>();
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        
        await _ctx.SaveChangesAsync(cancellationToken);
        
        return entity;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
        
        if (entity == null) return;
        
        _dbSet.Remove(entity);
        
        await _ctx.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync(new object[] { id }, cancellationToken) as T;

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);
        await _ctx.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllWithIncludesAsync(
        Func<IQueryable<T>, IQueryable<T>> include,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _dbSet.AsQueryable();
        
        query = include(query);
        
        return await query.AsNoTracking().ToListAsync(cancellationToken);
    }
}
