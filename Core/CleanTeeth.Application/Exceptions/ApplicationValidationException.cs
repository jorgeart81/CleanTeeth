using System;
using FluentValidation.Results;

namespace CleanTeeth.Application.Exceptions;

public class ApplicationValidationException : Exception
{
    public List<string> ValidationErrors { get; set; } = [];

    public ApplicationValidationException(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            ValidationErrors.Add(error.ErrorMessage);
        }
    }
}
