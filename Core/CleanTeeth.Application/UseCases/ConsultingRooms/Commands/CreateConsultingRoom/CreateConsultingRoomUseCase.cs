using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.UseCases.ConsultingRooms.Commands.CreateConsultingRoom;

public class CreateConsultingRoomUseCase(IConsultingRoomRepository repository)
{
    public async Task<Guid> Handle(CreateConsultingRoomCommand command)
    {
        ConsultingRoom newEntity = new() { Name = command.Name };
        ConsultingRoom? result = await repository.Add(newEntity);
        return result.Id;
    }
}
