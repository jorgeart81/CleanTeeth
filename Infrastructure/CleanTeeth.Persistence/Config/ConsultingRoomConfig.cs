using CleanTeeth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanTeeth.Persistence.Config;

public class ConsultingRoomConfig : IEntityTypeConfiguration<ConsultingRoom>
{
    public void Configure(EntityTypeBuilder<ConsultingRoom> builder)
    {
        builder.Property(prop => prop.Name)
            .HasMaxLength(150)
            .IsRequired();
    }
}