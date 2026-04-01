using CleanTeeth.Domain.Validations;

namespace CleanTeeth.Domain.Entities;

public class Dentist
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public required string Name
    {
        get;
        init => field = EnsureDomainRule.Ensure(value, ValidationRules.IsRequired, "The name is required");
    }
    public required string Email
    {
        get;
        init => field = EnsureDomainRule
                        .Ensure(value, ValidationRules.IsRequired, "The email is required")
                        .Ensure(ValidationRules.IsEmail, "The email is invalid");
    }
}
