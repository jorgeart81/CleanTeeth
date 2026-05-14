using System;

namespace CleanTeeth.Application.Contracts.Persistence;

public interface IUnitOfWork
{
    Task Persist();
    Task Reverse();
}
