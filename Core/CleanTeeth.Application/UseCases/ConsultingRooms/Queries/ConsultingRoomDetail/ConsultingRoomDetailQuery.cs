using CleanTeeth.Application.Utilities.Mediator;

namespace CleanTeeth.Application.UseCases.ConsultingRooms.Queries.ConsultingRoomDetail;

public record ConsultingRoomDetailQuery(Guid Id) : IRequest<ConsultingRoomDetailDTO>;
