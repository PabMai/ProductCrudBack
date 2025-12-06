using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ProductCrudBack.Services;

public interface IGenericService<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync(int id);
    Task<Dictionary<string, IEnumerable<string>>> GetModelStateErrorsAsync(ModelStateDictionary modelState);
}