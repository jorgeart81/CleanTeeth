using System;
using System.Reflection;
using CleanTeeth.Application.Exceptions;

namespace CleanTeeth.Application.Utilities.Mediator;

public class SimpleMediator(IServiceProvider serviceProvider) : IMediator
{
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        var handlerType = typeof(IRequestHandler<,>)
        .MakeGenericType(request.GetType(), typeof(TResponse));

        object handler = serviceProvider.GetService(handlerType)
            ?? throw new MediatorException($"No handler found for {request.GetType().Name}");

        MethodInfo method = handlerType.GetMethod("Handle")
            ?? throw new MediatorException($"No Handle method found for {handlerType.Name}");

        var parameters = method.GetParameters();
        object?[] args = [request];

        object? result = method.Invoke(handler, args);

        if (result is not Task<TResponse> task)
        {
            throw new MediatorException(
                $"The handler {handlerType.Name} did not return Task<{typeof(TResponse).Name}>");
        }

        return await task;
    }
}
