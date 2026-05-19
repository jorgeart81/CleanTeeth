using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities.Mediator;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.UseCases.ConsultingRooms.Queries.ConsultingRoomDetail;

public class ConsultingRoomDetailUseCase(IConsultingRoomRepository repository)
    : IRequestHandler<ConsultingRoomDetailQuery, ConsultingRoomDetailDTO>
{
    public async Task<ConsultingRoomDetailDTO> Handle(ConsultingRoomDetailQuery request)
    {
        ConsultingRoom consultingRoom = await repository.GetById(request.Id)
            ?? throw new NotFoundException();

        return consultingRoom.ToDTO();
    }
}
