using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Utilities.Mediator;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.UseCases.ConsultingRooms.Commands.CreateConsultingRoom;

public class CreateConsultingRoomUseCase(
    IConsultingRoomRepository repository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateConsultingRoomCommand, Guid>
{
    public async Task<Guid> Handle(CreateConsultingRoomCommand command)
    {
        ConsultingRoom newEntity = new() { Name = command.Name };

        try
        {
            ConsultingRoom? result = await repository.Add(newEntity);
            await unitOfWork.Persist();
            return result.Id;
        }
        catch (Exception)
        {
            await unitOfWork.Reverse();
            throw;
        }

    }
}
