using CleanTeeth.API.DTOs.ConsultingRoom;
using CleanTeeth.Application.UseCases.ConsultingRooms.Commands.CreateConsultingRoom;
using CleanTeeth.Application.UseCases.ConsultingRooms.Queries.ConsultingRoomDetail;
using CleanTeeth.Application.Utilities.Mediator;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CleanTeeth.API.Endpoints;

internal static class ConsultingRoomEndpoints
{
    extension(RouteGroupBuilder routeGroup)
    {
        internal RouteGroupBuilder MapConsultingRooms()
        {
            routeGroup.MapGet("/{id:guid}", GetById).WithName(nameof(GetById));
            routeGroup.MapPost("/", Create).DisableAntiforgery();

            return routeGroup;
        }
    }

    static async Task<Results<Ok<ConsultingRoomDetailDTO>, InternalServerError>> GetById(IMediator mediator, Guid id)
    {
        ConsultingRoomDetailQuery query = new(id);
        ConsultingRoomDetailDTO result = await mediator.Send(query);

        return TypedResults.Ok(result);
    }

    static async Task<Results<Ok<Guid>, InternalServerError>> Create(
        IMediator mediator,
        CreateConsultingRoomDTO dTO)
    {
        CreateConsultingRoomCommand command = new(Name: dTO.Name);
        var result = await mediator.Send(command);

        return TypedResults.Ok(result);
    }
}