using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Tests.Domain.Entities;

[TestClass]
public class DentistTest
{
    [TestMethod]
    public void SetName_NullValue_ThrowsDomainException()
    {
        Assert.Throws<BusinessRuleException>(() =>
        {
            _ = new Dentist { Name = null!, Email = new Email("test@domain.com") };
        });
    }

    [TestMethod]
    public void SetEmail_NullValue_ThrowsDomainException()
    {
        Assert.Throws<BusinessRuleException>(() =>
        {
            _ = new Dentist { Name = "Dentist name", Email = null! };
        });
    }
}
