using CleanTeeth.Application.Contracts.Persistence;

namespace CleanTeeth.Persistence.UnitOfWork;

public class UnitOfWorkEFCore(CleanTeethDBContext dBContext) : IUnitOfWork
{
    public async Task Persist()
    {
        await dBContext.SaveChangesAsync();
    }

    public Task Reverse()
    {
        return Task.CompletedTask;
    }
}