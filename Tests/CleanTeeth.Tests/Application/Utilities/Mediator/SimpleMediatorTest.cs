using System;
using CleanTeeth.Application.Utilities.Mediator;
using NSubstitute;

namespace CleanTeeth.Tests.Application.Utilities.Mediator;

[TestClass]
public class SimpleMediatorTest
{
    public class MockRequest : IRequest<string> { };
    public class MockHandle : IRequestHandler<MockRequest, string>
    {
        public Task<string> Handle(MockRequest request)
        {
            return Task.FromResult("Correct response");
        }
    }

    [TestMethod]
    public async Task Send_CallHandleMethod()
    {
        var requestMock = new MockRequest();
        var requestHandlerMock = Substitute.For<IRequestHandler<MockRequest, string>>();
        var serviceProvider = Substitute.For<IServiceProvider>();

        serviceProvider
            .GetService(typeof(IRequestHandler<MockRequest, string>))
            .Returns(requestHandlerMock);

        var mediator = new SimpleMediator(serviceProvider);

        var result = await mediator.Send(requestMock);

        await requestHandlerMock.Received(1).Handle(requestMock);
    }
}
