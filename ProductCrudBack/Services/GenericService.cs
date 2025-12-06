using Microsoft.AspNetCore.Mvc.ModelBinding;
using ProductCrudBack.Repositories;

namespace ProductCrudBack.Services;

public class GenericService<T> : IGenericService<T> where T : class
{
    protected readonly IGenericRepository<T> _repository;
    
    public GenericService(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    
    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public virtual async Task<T?> AddAsync(T entity)
    {
        await _repository.AddAsync(entity);
        return entity;
    }

    public virtual async Task<T?> UpdateAsync(T entity)
    {
        await _repository.UpdateAsync(entity);
        return entity;
    }

    public virtual async Task DeleteAsync(T entity)
    {
        await _repository.DeleteAsync(entity);
    }

    public virtual async Task<bool> ExistsAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity != null;
    }

    public virtual async Task<Dictionary<string, IEnumerable<string>>> GetModelStateErrorsAsync(ModelStateDictionary modelState)
    {
        return modelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    x => x.Key, 
                    x => x.Value.Errors.Select(x => x.ErrorMessage)
                );
    }
}
