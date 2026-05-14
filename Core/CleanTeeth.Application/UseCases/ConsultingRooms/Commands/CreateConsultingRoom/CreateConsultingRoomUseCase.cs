using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Domain.Entities;
using FluentValidation;

namespace CleanTeeth.Application.UseCases.ConsultingRooms.Commands.CreateConsultingRoom;

public class CreateConsultingRoomUseCase(
    IConsultingRoomRepository repository,
    IUnitOfWork unitOfWork,
    IValidator<CreateConsultingRoomCommand> validator)
{
    public async Task<Guid> Handle(CreateConsultingRoomCommand command)
    {
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            throw new ApplicationValidationException(validationResult);
        }

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
