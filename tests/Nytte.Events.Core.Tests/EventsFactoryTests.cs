using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Nytte.Events.Abstractions;
using Nytte.Events.Core.Tests.Models;
using Nytte.Testing;
using Shouldly;

namespace Nytte.Events.Core.Tests
{
    public class EventsFactoryTests : ServiceUnderTest<IEventsFactory, EventsFactory>
    {
        private Mock<IServiceProvider> _serviceProvider;
        private Mock<IServiceScope> _scope;
        private Mock<IEventTransporter> _transporter;
        private Mock<EventsOptions> _options;

        public override void Setup()
        {
            _serviceProvider = new Mock<IServiceProvider>();
            _scope = new Mock<IServiceScope>();
            _transporter = new Mock<IEventTransporter>();
            _serviceProvider.Setup(o => o.GetService(typeof(IEventTransporter))).Returns(_transporter.Object);
            var scopeFactory = new Mock<IServiceScopeFactory>();
            scopeFactory.Setup(o => o.CreateScope()).Returns(_scope.Object);
            _serviceProvider.Setup(o => o.GetService(typeof(IServiceScopeFactory))).Returns(scopeFactory.Object);
            _options = Mocker.GetMock<EventsOptions>();

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

        [Test]
        public void GetEventKey_Attribute_GetsKey()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            var key = sut.GetEventKey<TestEventAttribute>();

            //Assert
            key.ShouldBe(("main/TestEventAttribute"));
        }
        
        [Test]
        public void GetEventKey_NoAttribute_Throws()
        {
            //Arrange
            var sut = CreateSut();

            //Act
            //Assert
            Assert.Throws<InvalidOperationException>(() => sut.GetEventKey<TestEvent>());
        }

        [Test]
        public void GetEventKey_NoOwnerMode_GetsKey()
        {
            //Arrange
            var sut = CreateSut();
            _options.Object.UseOwnersInKeys = false;
            
            //Act
            var key = sut.GetEventKey<TestEvent>();
            
            //Assert
            key.ShouldBe("TestEvent");
        }

        [Test]
        public void CreateRegistration_Always_CreatesRegistration()
        {
            //Arrange
            var sut = CreateSut();
            var key = "key";
            var handler = new ScopedEventHandlerAsync((provider, @event) => Task.CompletedTask);
            
            //Act
            var res = sut.CreateRegistration<TestEvent>(handler, key);
            
            //Assert
            res.Key.ShouldBe(key);
            res.AsyncHandler.ShouldBe(handler);
            res.Type.ShouldBe(typeof(TestEvent));
        }

    }
}