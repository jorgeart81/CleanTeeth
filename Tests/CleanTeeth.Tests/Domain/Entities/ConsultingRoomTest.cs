using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Exceptions;

namespace CleanTeeth.Tests.Domain.Entities;

[TestClass]
public class ConsultingRoomTest
{
    [TestMethod]
    public void SetName_NullValue_ThrowsDomainException()
    {
        Assert.Throws<BusinessRuleException>(() =>
        {
            _ = new ConsultingRoom { Name = null! };
        });
    }
}
