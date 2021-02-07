using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Nytte.Events.Abstractions;
using Nytte.Events.Core.Tests.Models;
using Nytte.Testing;

namespace Nytte.Events.Core.Tests
{
    public class EventsFactoryTests : ServiceUnderTest<IEventsFactory, EventsFactory>
    {
        private Mock<IServiceProvider> _serviceProvider;
        private Mock<IServiceScope> _scope;
        private Mock<IEventTransporter> _transporter;

        public override void Setup()
        {
            _serviceProvider = new Mock<IServiceProvider>();
            _scope = new Mock<IServiceScope>();
            _transporter = new Mock<IEventTransporter>();
            _serviceProvider.Setup(o => o.GetService(typeof(IEventTransporter))).Returns(_transporter.Object);
            var scopeFactory = new Mock<IServiceScopeFactory>();
            scopeFactory.Setup(o => o.CreateScope()).Returns(_scope.Object);
            _serviceProvider.Setup(o => o.GetService(typeof(IServiceScopeFactory))).Returns(scopeFactory.Object);

        }

        [Test]
        public void CreateHandler_NoHandler_Throws()
        {
            //Arrange
            var sut = CreateSut();
            var packedEvent = "packedEvent";
            _transporter.Setup(o => o.UnPackAsync<TestEvent>(packedEvent)).ReturnsAsync(new TestEvent());

            //Act
            var handler = sut.CreateHandler<TestEvent>();
            
            //Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => handler(_serviceProvider.Object, packedEvent));
        }

        [Test]
        public async Task CreateHandler_Handler_CallsHandler()
        {
            //Arrange
            var sut = CreateSut();
            var packedEvent = "packedEvent";
            var handlerMock = new Mock<IEventHandler<TestEvent>>();
            var ev = new TestEvent();
            _serviceProvider.Setup(o => o.GetService(typeof(IEventHandler<TestEvent>))).Returns(handlerMock.Object);
            _transporter.Setup(o => o.UnPackAsync<TestEvent>(packedEvent)).ReturnsAsync(ev);

            //Act
            var handler = sut.CreateHandler<TestEvent>();
            await handler(_serviceProvider.Object, packedEvent);

            //Assert
            handlerMock.Verify(o => o.HandleAsync(ev));
        }
        
    }
}