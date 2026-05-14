using CleanTeeth.Application.Contracts.Persistence;
using CleanTeeth.Application.Contracts.Repositories;
using CleanTeeth.Application.UseCases.ConsultingRooms.Commands.CreateConsultingRoom;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using DomainConsultingRoom = CleanTeeth.Domain.Entities.ConsultingRoom;

namespace CleanTeeth.Tests.Application.UseCases.ConsultingRoom;

[TestClass]
public class CreateConsultingRoomUseCaseTest
{
    // Test dependencies - using NSubstitute for mocking
    private IConsultingRoomRepository repository;
    private IUnitOfWork unitOfWork;
    private CreateConsultingRoomUseCase useCase;

    /// <summary>
    /// Test initialization - runs before each test (like @Before in JUnit).
    /// Sets up mock objects for all dependencies using NSubstitute.
    /// </summary>
    /// <remarks>
    /// <h3>Why Substitude.For&lt;T&gt;()?</h3>
    /// <para>Instead of using real implementations, we create fake objects:</para>
    /// <code>Substitute.For&lt;IConsultingRoomRepository&gt;()</code>
    /// <para>This creates a mock that:</para>
    /// <list type="number">
    ///   <item>Implements the interface</item>
    ///   <item>Returns default values (null, empty collections)</item>
    ///   <item>Records all method calls for verification</item>
    /// </list>
    /// <h3>Constructor Injection</h3>
    /// <para>We pass mocks to the use case constructor - this is Dependency Injection.
    /// The use case doesn't know it's receiving mocks vs real objects.</para>
    /// </remarks>
    [TestInitialize]
    public void Setup()
    {
        // Create mock objects for interfaces
        // Substitute.For<T>() creates a fake implementation of interface T
        repository = Substitute.For<IConsultingRoomRepository>();
        unitOfWork = Substitute.For<IUnitOfWork>();

        // Inject dependencies into the use case
        // The use case receives mocks, but behaves as if they're real
        useCase = new(repository, unitOfWork);
    }

    /// <summary>
    /// Tests successful execution with a valid command.
    /// Step-by-step verification of the happy path:
    /// 1. Create a valid command with name "Cardiology"
    /// 2. Configure mock: repository.Add returns a consulting room entity
    /// 3. Execute the use case with the command
    /// 4. Verify: repository.Add was called once (persists the entity)
    /// 5. Verify: unitOfWork.Persist was called (saves changes)
    /// 6. Verify: result is a valid Guid (not empty)
    /// </summary>
    [TestMethod]
    public async Task Handle_ValidCommand_GetConsultingRoomId()
    {
        // Step 1: Arrange - Create test data (command with valid name)
        CreateConsultingRoomCommand command = new(Name: "Cardiology");

        // Step 2: Configure mock behavior - repository simulates successful save
        // When Add is called, return a ConsultingRoom with an Id
        DomainConsultingRoom consultingRoom = new() { Name = "Cardiology" };
        repository.Add(Arg.Any<DomainConsultingRoom>()).Returns(consultingRoom);

        // Step 3: Act - Execute the use case (the code we want to test)
        var result = await useCase.Handle(command);

        // Step 4-6: Assert - Verify the interactions happened correctly

        // Verify repository added the entity (Arg.Any ignores specific object values)
        await repository.Received(1).Add(Arg.Any<DomainConsultingRoom>());

        // Verify transaction was committed
        await unitOfWork.Received(1).Persist();

        // Verify result is a valid ID (not Guid.Empty)
        Assert.AreNotEqual(Guid.Empty, result);
    }

    /// <summary>
    /// Tests transaction rollback when repository throws an exception.
    /// Step-by-step verification of error handling:
    /// 1. Create valid command with name "Cardiology"
    /// 2. Configure mock: repository.Add throws an exception (simulates DB error)
    /// 3. Execute use case - exception should propagate
    /// 4. Verify: unitOfWork.Reverse was called (rollback transaction)
    /// Why? Ensures no partial/inconsistent data remains in database
    /// </summary>
    [TestMethod]
    public async Task Handle_WhenError_DoReverse()
    {
        // Step 1: Arrange - Create valid command
        CreateConsultingRoomCommand command = new(Name: "Cardiology");

        // Step 2: Configure mock - repository FAILS (simulates database error)
        // This could be: connection failure, constraint violation, timeout, etc.
        repository.Add(Arg.Any<DomainConsultingRoom>()).Throws<Exception>();

        // Step 3: Act & Assert - Exception propagates up to caller
        await Assert.ThrowsAsync<Exception>(async () =>
        {
            var result = await useCase.Handle(command);
        });

        // Step 4: Verify rollback was executed
        // Important: even though exception happened, we must clean up
        // Reverse() cancels any pending transaction changes
        await unitOfWork.Received(1).Reverse();
    }
}
