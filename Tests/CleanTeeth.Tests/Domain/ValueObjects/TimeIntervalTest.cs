using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Tests.Domain.ValueObjects;

[TestClass]
public class TimeIntervalTest
{
    [TestMethod]
    public void Constructor_StartDateLaterThanEndDate_ThrowsDomainException()
    {
        Assert.Throws<BusinessRuleException>(() =>
        {
            _ = new TimeInterval(DateTime.UtcNow, DateTime.UtcNow.AddDays(-1));
        });
    }

    [TestMethod]
    public void Constructor_ValidParams_CreatesTimeInterval()
    {
        var start = DateTime.UtcNow;
        var end = start.AddMinutes(60);
        var timeInterval = new TimeInterval(start, end);

        Assert.AreEqual(start, timeInterval.Start);
        Assert.AreEqual(end, timeInterval.End);
    }
}
