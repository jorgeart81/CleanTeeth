using System;

namespace CleanTeeth.Domain.Entities;

public class Appointment
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }
    public Guid DentistId { get; private set; }
    public Guid ConsultingRoomId { get; private set; }
    public ApartmentState State { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public Patient? Patient { get; private set; }
    public Dentist? Dentist { get; private set; }
    public ConsultingRoom? ConsultingRoom { get; private set; }
}
