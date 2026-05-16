using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Application.UseCases.ConsultingRooms.Queries.ConsultingRoomDetail;

public static class MapperExtensions
{
    extension(ConsultingRoom consultingRoom)
    {
        public ConsultingRoomDetailDTO ToDTO() => new(Id: consultingRoom.Id, Name: consultingRoom.Name);
    }
}