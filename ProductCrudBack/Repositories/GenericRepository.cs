using Microsoft.EntityFrameworkCore;
using ProductCrudBack.Data;
using System.Linq.Expressions;

namespace ProductCrudBack.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ProductCrudDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ProductCrudDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.ToListAsync();

    public async Task<T?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        => await _dbSet.Where(predicate).ToListAsync();

    public async Task AddAsync(T entity)
        => await _dbSet.AddAsync(entity);

    public Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveAsync()
        => await _context.SaveChangesAsync();

}