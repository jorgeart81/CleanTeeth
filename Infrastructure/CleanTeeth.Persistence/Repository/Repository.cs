using CleanTeeth.Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Persistence.Repository;

public abstract class Repository<T>(CleanTeethDBContext dBContext) : IRepository<T> where T : class
{
    public Task<T> Add(T entity)
    {
        dBContext.Add(entity);
        return Task.FromResult(entity);
    }

    public Task Delete(T entity)
    {
        dBContext.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await dBContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetById(Guid id)
    {
        return await dBContext.Set<T>().FindAsync(id);
    }

    public Task Update(T entity)
    {
        dBContext.Update(entity);
        return Task.CompletedTask;
    }
}