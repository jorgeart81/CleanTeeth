using System;
using FluentValidation;

namespace CleanTeeth.Application.UseCases.ConsultingRooms.Commands.CreateConsultingRoom;

public class CreateConsultingRoomValidator : AbstractValidator<CreateConsultingRoomCommand>
{
    public CreateConsultingRoomValidator()
    {
        RuleFor(p => p.Name).NotEmpty().WithMessage("The {PropertyName} field is required");
    }
}
