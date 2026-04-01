using CleanTeeth.Domain.Enums;
using CleanTeeth.Domain.Validations;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Domain.Entities;

public class Appointment
{
    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public required Guid PatientId { get; init; }
    public required Guid DentistId { get; init; }
    public required Guid ConsultingRoomId { get; init; }
    public AppointmentStatus Status { get; private set; } = AppointmentStatus.Scheduled;
    public required TimeInterval TimeInterval
    {
        get;
        init => field = EnsureDomainRule.Ensure(value, interval => interval.Start >= DateTime.UtcNow, "The start date cannot be earlier than the current date");
    }
    public Patient? Patient { get; init; }
    public Dentist? Dentist { get; init; }
    public ConsultingRoom? ConsultingRoom { get; init; }

    public void Cancel()
    {
        _ = EnsureDomainRule.Ensure(Status, status => status is AppointmentStatus.Scheduled, "You can only cancel a scheduled appointment");
        Status = AppointmentStatus.Canceled;
    }
    public void Complete()
    {
        _ = EnsureDomainRule.Ensure(Status, status => status is AppointmentStatus.Scheduled, "You can only complete a scheduled appointment");
        Status = AppointmentStatus.Completed;
    }
}
