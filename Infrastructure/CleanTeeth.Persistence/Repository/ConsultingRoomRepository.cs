using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Domain.Entities;

namespace CleanTeeth.Persistence.Repository;

public class ConsultingRoomRepository(CleanTeethDBContext dBContext) :
    Repository<ConsultingRoom>(dBContext), IConsultingRoomRepository
{
}