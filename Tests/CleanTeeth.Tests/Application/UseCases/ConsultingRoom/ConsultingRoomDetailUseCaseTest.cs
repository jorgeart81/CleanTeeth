using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.UseCases.ConsultingRooms.Queries.ConsultingRoomDetail;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using DomainConsultingRoom = CleanTeeth.Domain.Entities.ConsultingRoom;

namespace CleanTeeth.Tests.Application.UseCases.ConsultingRoom;

[TestClass]
public class ConsultingRoomDetailUseCaseTest
{
    // Test dependencies - using NSubstitute for mocking
    private IConsultingRoomRepository repository;
    private ConsultingRoomDetailUseCase useCase;

    [TestInitialize]
    public void Setup()
    {
        // Create mock objects for interfaces
        // Substitute.For<T>() creates a fake implementation of interface T
        repository = Substitute.For<IConsultingRoomRepository>();

        // Inject dependencies into the use case
        // The use case receives mocks, but behaves as if they're real
        useCase = new(repository);
    }


    [TestMethod]
    public async Task Handle_ConsultingRoomExist_ReturnDTO()
    {
        var consultingRoom = new DomainConsultingRoom() { Name = "Room A" };
        Guid id = consultingRoom.Id;
        ConsultingRoomDetailQuery query = new(Id: id);

        repository.GetById(id).Returns(consultingRoom);

        ConsultingRoomDetailDTO result = await useCase.Handle(query);

        Assert.IsNotNull(result);
        Assert.AreEqual(id, result.Id);
        Assert.AreEqual("Room A", result.Name);
    }

    [TestMethod]
    public async Task Handle_ConsultingRoomNotExist_ThrowsException()
    {
        Guid id = Guid.NewGuid();
        ConsultingRoomDetailQuery query = new(Id: id);

        repository.GetById(id).ReturnsNull();

        await Assert.ThrowsAsync<NotFoundExcepetion>(async () =>
     {
         await useCase.Handle(query);
     });

    }

}
