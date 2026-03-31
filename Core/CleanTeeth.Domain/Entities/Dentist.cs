namespace CleanTeeth.Domain.Entities;

public class Dentist
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
}
