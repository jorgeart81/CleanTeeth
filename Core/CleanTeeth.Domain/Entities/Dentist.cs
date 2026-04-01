using CleanTeeth.Domain.Validations;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Domain.Entities;

public class Dentist
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public required string Name
    {
        get;
        init => field = EnsureDomainRule.Ensure(value, ValidationRules.IsRequired, "The name is required");
    }
    public required Email Email { get; init; }
}
