using System.Reflection;
using CleanTeeth.Application.Exceptions;
using FluentValidation;
using FluentValidation.Results;

namespace CleanTeeth.Application.Utilities.Mediator;

public class SimpleMediator(IServiceProvider serviceProvider) : IMediator
{
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    {
        await RequestValidationMethod(request);

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

    private async Task RequestValidationMethod<TResponse>(IRequest<TResponse> request)
    {
        var validatorType = typeof(IValidator<>).MakeGenericType(request.GetType());
        var validator = serviceProvider.GetService(validatorType);

        if (validator is not null)
        {
            MethodInfo? validationMethod = validatorType.GetMethod("ValidateAsync");
            var taskValidate = (Task)validationMethod!.Invoke(validator, [request, CancellationToken.None])!;

            await taskValidate.ConfigureAwait(false);

            var taskResult = taskValidate.GetType().GetProperty("Result");
            var validationResult = (ValidationResult)taskResult!.GetValue(taskValidate)!;

            if (!validationResult.IsValid)
            {
                throw new ApplicationValidationException(validationResult);
            }
        }
    }
}
