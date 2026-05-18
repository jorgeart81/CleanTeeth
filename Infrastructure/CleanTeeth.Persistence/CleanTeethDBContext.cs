using CleanTeeth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanTeeth.Persistence;

public class CleanTeethDBContext : DbContext
{
    public CleanTeethDBContext(DbContextOptions<CleanTeethDBContext> options) : base(options)
    {
    }

    protected CleanTeethDBContext()
    {
    }

    public DbSet<ConsultingRoom> MyProperty { get; set; }
}