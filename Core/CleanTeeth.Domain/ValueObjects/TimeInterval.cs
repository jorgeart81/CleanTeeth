using CleanTeeth.Domain.Validations;

namespace CleanTeeth.Domain.ValueObjects;

public class TimeInterval
{
    public DateTime Start { get; }
    public DateTime End { get; }

    public TimeInterval(DateTime start, DateTime end)
    {
        Start = EnsureDomainRule.Ensure(start, starDate => starDate < end, "The start date cannot be later than the end date");
        End = end;
    }
}
