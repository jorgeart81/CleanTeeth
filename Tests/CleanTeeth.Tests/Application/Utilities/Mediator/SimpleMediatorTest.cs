using System;
using CleanTeeth.Application.Exceptions;
using CleanTeeth.Application.Utilities.Mediator;
using FluentValidation;
using NSubstitute;

namespace CleanTeeth.Tests.Application.Utilities.Mediator;

[TestClass]
public class SimpleMediatorTest
{
    public class MockRequest : IRequest<string>
    {
        public required string Name { get; set; }
    };
    public class MockHandle : IRequestHandler<MockRequest, string>
    {
        public Task<string> Handle(MockRequest request)
        {
            return Task.FromResult("Correct response");
        }
    }
    public class MockRequestValidator : AbstractValidator<MockRequest>
    {
        public MockRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }

    [TestMethod]
    public async Task Send_CallHandleMethod()
    {
        var requestMock = new MockRequest() { Name = "Test name" };
        var requestHandlerMock = Substitute.For<IRequestHandler<MockRequest, string>>();
        var serviceProvider = Substitute.For<IServiceProvider>();

        serviceProvider
            .GetService(typeof(IRequestHandler<MockRequest, string>))
            .Returns(requestHandlerMock);

        var mediator = new SimpleMediator(serviceProvider);

        var result = await mediator.Send(requestMock);

        await requestHandlerMock.Received(1).Handle(requestMock);
    }

    [TestMethod]
    public async Task Send_NoRegisteredHandler_ThrowsException()
    {
        var requestMock = new MockRequest() { Name = "Test name" };
        var requestHandlerMock = Substitute.For<IRequestHandler<MockRequest, string>>();
        var serviceProvider = Substitute.For<IServiceProvider>();

        var mediator = new SimpleMediator(serviceProvider);

        await Assert.ThrowsAsync<MediatorException>(async () =>
        {
            await mediator.Send(requestMock);
        });
    }

    [TestMethod]
    public async Task Handle_InvalidCommand_ThrowsException()
    {
        var requestMock = new MockRequest() { Name = "" };
        var serviceProvider = Substitute.For<IServiceProvider>();
        var validator = new MockRequestValidator();

        serviceProvider
            .GetService(typeof(IValidator<MockRequest>))
            .Returns(validator);

        var mediator = new SimpleMediator(serviceProvider);

        await Assert.ThrowsAsync<ApplicationValidationException>(async () =>
        {
            await mediator.Send(requestMock);
        });
    }
}
