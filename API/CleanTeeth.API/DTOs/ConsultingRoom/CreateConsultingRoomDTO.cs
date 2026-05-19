using System.ComponentModel.DataAnnotations;

namespace CleanTeeth.API.DTOs.ConsultingRoom;

internal record CreateConsultingRoomDTO(
    [Required]
    [StringLength(150)]
    string Name
);