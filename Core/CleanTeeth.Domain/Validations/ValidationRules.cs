using System.Net.Mail;

namespace CleanTeeth.Domain.Validations;

public static class ValidationRules
{
    public static Func<string, bool> IsRequired => value => !string.IsNullOrWhiteSpace(value);
    public static Func<string, bool> IsEmail => value => MailAddress.TryCreate(value, out _);
}
