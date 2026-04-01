using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Tests.Domain.ValueObjects;

[TestClass]
public class EmailTest
{
    [TestMethod]
    public void Constructor_NullEmail_ThrowsDomainException()
    {
        Assert.Throws<BusinessRuleException>(() =>
        {
            _ = new Email(null!);
        });
    }

    [TestMethod]
    [DataRow("user_domain.com")]
    [DataRow("user@@domain.com")]
    [DataRow("@domain.com")]
    [DataRow("")]
    [DataRow(" ")]
    public void Constructor_InvalidEmail_ThrowsDomainException(string invalidEmail)
    {
        Assert.Throws<BusinessRuleException>(() =>
        {
            var email = new Email(invalidEmail);
        });
    }

    [TestMethod]
    [DataRow("user@dominio.com")]
    public void Constructor_ValidEmail_CreatesEmail(string validEmail)
    {
        var email = new Email(validEmail);
        Assert.AreEqual(validEmail, email.Value);
    }
}
