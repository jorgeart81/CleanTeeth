using System;

namespace CleanTeeth.Application.Exceptions;

public class NotFoundExcepetion : Exception
{
    public NotFoundExcepetion() : base()
    {
    }
    public NotFoundExcepetion(string? message) : base(message)
    {
    }
}
