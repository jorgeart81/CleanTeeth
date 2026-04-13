using CleanTeeth.Domain.Entities;
using CleanTeeth.Domain.Enums;
using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Tests.Domain.Entities;

[TestClass]
public class AppointmentTest
{
    private readonly Guid _patientId = Guid.NewGuid();
    private readonly Guid _dentistId = Guid.NewGuid();
    private readonly Guid _consultingRoomId = Guid.NewGuid();
    private readonly TimeInterval _timeInterval = new(DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

    [TestMethod]
    public void Create_ValidAppointment_StatusScheduled()
    {
        var appointment = new Appointment
        {
            PatientId = _patientId,
            DentistId = _dentistId,
            ConsultingRoomId = _consultingRoomId,
            TimeInterval = _timeInterval
        };

        Assert.AreEqual(_patientId, appointment.PatientId);
        Assert.AreEqual(_dentistId, appointment.DentistId);
        Assert.AreEqual(_consultingRoomId, appointment.ConsultingRoomId);
        Assert.AreEqual(_timeInterval, appointment.TimeInterval);
        Assert.AreEqual(AppointmentStatus.Scheduled, appointment.Status);
        Assert.AreNotEqual(Guid.Empty, appointment.Id);
    }

    [TestMethod]
    public void CreateAppointment_WithPastStartDate_ThrowsDomainException()
    {
        TimeInterval interval = new(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(2));

        Assert.Throws<BusinessRuleException>(() =>
        {
            _ = new Appointment
            {
                PatientId = _patientId,
                DentistId = _dentistId,
                ConsultingRoomId = _consultingRoomId,
                TimeInterval = interval
            };
        });
    }
}
