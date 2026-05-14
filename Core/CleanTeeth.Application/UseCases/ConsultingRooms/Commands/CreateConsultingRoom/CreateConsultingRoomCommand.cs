using CleanTeeth.Application.Utilities.Mediator;

namespace CleanTeeth.Application.UseCases.ConsultingRooms.Commands.CreateConsultingRoom;

public record CreateConsultingRoomCommand(string Name) : IRequest<Guid>;
