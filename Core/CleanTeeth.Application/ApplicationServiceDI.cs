using CleanTeeth.Application.UseCases.ConsultingRooms.Commands.CreateConsultingRoom;
using CleanTeeth.Application.UseCases.ConsultingRooms.Queries.ConsultingRoomDetail;
using CleanTeeth.Application.Utilities.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace CleanTeeth.Application;

public static class ApplicationServiceDI
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAplicationServices()
        {
            services.AddTransient<IMediator, SimpleMediator>();
            services.AddScoped<IRequestHandler<CreateConsultingRoomCommand, Guid>,
                                CreateConsultingRoomUseCase>();
            services.AddScoped<IRequestHandler<ConsultingRoomDetailQuery, ConsultingRoomDetailDTO>,
                                ConsultingRoomDetailUseCase>();

            return services;
        }
    }
}