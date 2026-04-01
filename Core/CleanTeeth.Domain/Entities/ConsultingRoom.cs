using CleanTeeth.Domain.Validations;

namespace CleanTeeth.Domain.Entities;

public class ConsultingRoom
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public required string Name
    {
        get;
        init => field = EnsureDomainRule.Ensure(value, ValidationRules.IsRequired, "The name is required");
    }
}
