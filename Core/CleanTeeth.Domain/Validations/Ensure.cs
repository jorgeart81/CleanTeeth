using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Domain.Validations;

public static class EnsureDomainRule
{
    extension<T>(T value)
    {
        public T Ensure(Func<T, bool> predicate, string message)
        {
            if (!predicate(value)) throw new BusinessRuleException(message);
            return value;
        }
    }
}
