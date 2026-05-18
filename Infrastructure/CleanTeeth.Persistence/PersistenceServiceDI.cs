using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTeeth.Persistence;

public static class PersistenceServiceDI
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddPersistenceServices()
        {
            services.AddDbContext<CleanTeethDBContext>(options =>
                options.UseNpgsql("name=CleanTeethConnectionString"));

            services.AddScoped<IConsultingRoomRepository, ConsultingRoomRepository>();

            return services;
        }
    }
}