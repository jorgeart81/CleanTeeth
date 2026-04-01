using CleanTeeth.Domain.Validations;

namespace CleanTeeth.Domain.ValueObjects;

public record Email
{
    public string Value { get; }
    public Email(string email)
    {
        Value = EnsureDomainRule
                        .Ensure(email, ValidationRules.IsRequired, $"The {nameof(email)} is required")
                        .Ensure(ValidationRules.IsEmail, $"The {nameof(email)}  is invalid");
    }
}
