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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CleanTeethDBContext).Assembly);
    }

    public DbSet<ConsultingRoom> ConsultingRooms { get; set; }
}