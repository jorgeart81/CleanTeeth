using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.UseCases.ConsultingRooms.Commands.CreateConsultingRoom;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using DomainConsultingRoom = CleanTeeth.Domain.Entities.ConsultingRoom;

namespace CleanTeeth.Tests.Application.UseCases.ConsultingRoom;

[TestClass]
public class CreateConsultingRoomUseCaseTest
{
    private IConsultingRoomRepository repository;
    private IValidator<CreateConsultingRoomCommand> validator;
    private IUnitOfWork unitOfWork;
    private CreateConsultingRoomUseCase useCase;

    [TestInitialize]
    public void Setup()
    {
        repository = Substitute.For<IConsultingRoomRepository>();
        validator = Substitute.For<IValidator<CreateConsultingRoomCommand>>();
        unitOfWork = Substitute.For<IUnitOfWork>();

        useCase = new(repository, unitOfWork, validator);
    }

    [TestMethod]
    public async Task Handle_ValidCommand_GetConsultingRoomId()
    {
        CreateConsultingRoomCommand command = new(Name: "Cardiology");

        validator.ValidateAsync(command).Returns(new ValidationResult());

        DomainConsultingRoom consultingRoom = new() { Name = "Cardiology" };
        repository.Add(Arg.Any<DomainConsultingRoom>()).Returns(consultingRoom);

        var result = await useCase.Handle(command);

        await validator.Received(1).ValidateAsync(command);
        await repository.Received(1).Add(Arg.Any<DomainConsultingRoom>());
        await unitOfWork.Received(1).Persist();

        Assert.AreNotEqual(Guid.Empty, result);
    }

    [TestMethod]
    public async Task Handle_InvalidCommand_ThrowsException()
    {
        CreateConsultingRoomCommand command = new(Name: "");

        ValidationResult invalidResult = new([new ValidationFailure("Name", "The name is required.")]);
        validator.ValidateAsync(command).Returns(invalidResult);

        await Assert.ThrowsAsync<ApplicationValidationException>(async () =>
        {
            await useCase.Handle(command);
        });

        await repository.DidNotReceive().Add(Arg.Any<DomainConsultingRoom>());
    }

    [TestMethod]
    public async Task Handle_WhenError_DoReverse()
    {
        CreateConsultingRoomCommand command = new(Name: "Cardiology");

        validator.ValidateAsync(command).Returns(new ValidationResult());
        repository.Add(Arg.Any<DomainConsultingRoom>()).Throws<Exception>();

        await Assert.ThrowsAsync<Exception>(async () =>
        {
            var result = await useCase.Handle(command);
        });

        await unitOfWork.Received(1).Reverse();
    }
}
