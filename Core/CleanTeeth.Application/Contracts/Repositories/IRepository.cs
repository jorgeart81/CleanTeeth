using System;

namespace CleanTeeth.Application.Contracts.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> Add(T entity);
    Task<T?> GetById(Guid id);
    Task<IEnumerable<T>> GetAll();
    Task Update(T entity);
    Task Delete(T entity);
}
